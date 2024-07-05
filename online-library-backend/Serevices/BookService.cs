using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Interfaces;

namespace OnlineLibrary.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookService(LibraryDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BookWithGenresDto> GetBookBySlug(string slug, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

        var existingBook = await _dbContext.Books.AsNoTracking()
                               .Include(book => book.Author)
                               .Include(book => book.Genres)
                               .ThenInclude(bg => bg.Genre)
                               .FirstOrDefaultAsync(existingBook => existingBook.Slug == slug, cancellationToken)
                           ?? throw new ArgumentException("Book not found.");

        return BookWithGenresToDto(existingBook);
    }

    public async Task<List<BookWithGenresDto>> GetAllBooks(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

        string? searchTerm = _httpContextAccessor.HttpContext.Request.Query["searchTerm"];

        if (searchTerm == null) searchTerm = "";

        var books = await _dbContext.Books.AsNoTracking()
            .Include(book => book.Author)
            .Include(book => book.Genres)
            .ThenInclude(bg => bg.Genre)
            .Where(
                book =>
                    book.Title.Contains(searchTerm)
                    || book.Author.Name.Contains(searchTerm)
                    || book.Author.Surname.Contains(searchTerm)
                    || book.Author.SecondName.Contains(searchTerm)
                    || book.Author.Country.Contains(searchTerm)
                    || book.Genres.Any(genre =>
                        genre.Genre.Name.Contains(searchTerm)
                    ))
            .ToListAsync(cancellationToken);

        return MapListBooksToDto(books);
    }

    // Helpful methods

    public static List<BookDto> MapBooksToDto(List<Book> books)
    {
        List<BookDto> result = new();

        foreach (var book in books)
            result.Add(new BookDto(
                book.Id,
                book.Slug,
                book.Title,
                book.Description,
                book.CoverUrl,
                AuthorService.AuthorToDto(book.Author)
            ));

        return result;
    }

    public static BookWithGenresDto BookWithGenresToDto(Book book)
    {
        return new BookWithGenresDto(
            book.Id,
            book.Slug,
            book.Title,
            book.Description,
            book.CoverUrl,
            AuthorService.AuthorToDto(book.Author),
            GenreService.MapGenresToDto(book.Genres)
        );
    }

    public static List<BookWithGenresDto> MapListBooksToDto(List<Book> books)
    {
        List<BookWithGenresDto> booksListDto = new();

        foreach (var book in books) booksListDto.Add(BookWithGenresToDto(book));

        return booksListDto;
    }
}
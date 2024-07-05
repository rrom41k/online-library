using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Interfaces;

namespace OnlineLibrary.Services;

public class GenreService : IGenreService
{
    private readonly LibraryDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GenreService(LibraryDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenreWithBooksDto> GetGenreBySlug(string slug, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

        var existingGenre = await _dbContext.Genres.AsNoTracking()
                                .Include(genre => genre.Books)
                                .ThenInclude(bg => bg.Book.Author)
                                .FirstOrDefaultAsync(existingGenre => existingGenre.Slug == slug, cancellationToken)
                            ?? throw new ArgumentException("Genre not found.");

        return GenreWithBooksToDto(existingGenre);
    }

    public async Task<List<GenreWithBooksDto>> GetAllGenres(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

        string? searchTerm = _httpContextAccessor.HttpContext.Request.Query["searchTerm"];

        if (searchTerm == null) searchTerm = "";

        var genres = await _dbContext.Genres.AsNoTracking()
            .Include(genre => genre.Books)
            .ThenInclude(bg => bg.Book)
            .ThenInclude(book => book.Author)
            .Where(
                genre =>
                    genre.Name.Contains(searchTerm)
                    || genre.Slug.Contains(searchTerm))
            .ToListAsync(cancellationToken);

        return MapGenresWithBooksToDto(genres);
    }

    // Helpful methods

    public static GenreDto GenreToDto(Genre genre)
    {
        return new GenreDto(
            genre.Id,
            genre.Name,
            genre.Slug);
    }

    public static List<GenreDto> MapGenresToDto(ICollection<BookGenre> genres)
    {
        {
            List<GenreDto> genresListDto = new();

            foreach (var bookGenre in genres) genresListDto.Add(GenreToDto(bookGenre.Genre));

            return genresListDto;
        }
    }

    public static GenreWithBooksDto GenreWithBooksToDto(Genre genre)
    {
        List<Book> books = new();
        foreach (var bg in genre.Books) books.Add(bg.Book);

        return new GenreWithBooksDto(
            genre.Id,
            genre.Name,
            genre.Slug,
            BookService.MapBooksToDto(books)
        );
    }

    private static List<GenreWithBooksDto> MapGenresWithBooksToDto(List<Genre> genres)
    {
        List<GenreWithBooksDto> genresListDto = new();

        foreach (var genre in genres) genresListDto.Add(GenreWithBooksToDto(genre));

        return genresListDto;
    }
}
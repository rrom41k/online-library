using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Interfaces;

namespace OnlineLibrary.Services;

public class AuthorService : IAuthorService
{
    private readonly LibraryDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorService(LibraryDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthorWithBooksDto> GetAuthorBySlug(string slug, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

        var existingAuthor = await _dbContext.Authors.AsNoTracking().Include(author => author.Books)
                                 .FirstOrDefaultAsync(existingAuthor => existingAuthor.Slug == slug, cancellationToken)
                             ?? throw new ArgumentException("Author not found.");

        return AuthorWithBooksToDto(existingAuthor);
    }

    public async Task<List<AuthorWithBooksDto>> GetAllAuthors(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

        string? searchTerm = _httpContextAccessor.HttpContext.Request.Query["searchTerm"];

        if (searchTerm == null) searchTerm = "";

        var authors = await _dbContext.Authors.AsNoTracking()
            .Where(
                Author =>
                    Author.Name.Contains(searchTerm)
                    || Author.Slug.Contains(searchTerm)).Include(authors => authors.Books)
            .ToListAsync(cancellationToken);

        return MapListAuthorsToDto(authors);
    }

    // Helpful methods
    public static AuthorDto AuthorToDto(Author author)
    {
        return new AuthorDto(
            author.Id,
            author.Slug,
            author.Name,
            author.Surname,
            author.SecondName,
            author.Description,
            author.Country,
            author.ImageUrl,
            author.DateOfBirth,
            author.DateOfDeath
        );
    }

    public static AuthorWithBooksDto AuthorWithBooksToDto(Author author)
    {
        return new AuthorWithBooksDto(
            author.Id,
            author.Slug,
            author.Name,
            author.Surname,
            author.SecondName,
            author.Description,
            author.Country,
            author.ImageUrl,
            author.DateOfBirth,
            author.DateOfDeath,
            BookService.MapBooksToDto(new List<Book>(author.Books))
        );
    }

    public static List<AuthorWithBooksDto> MapListAuthorsToDto(List<Author> authors)
    {
        List<AuthorWithBooksDto> authorsListDto = new();

        foreach (var author in authors) authorsListDto.Add(AuthorWithBooksToDto(author));

        return authorsListDto;
    }
}
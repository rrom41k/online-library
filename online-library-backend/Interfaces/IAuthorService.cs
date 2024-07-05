namespace OnlineLibrary.Interfaces;

public interface IAuthorService
{
    Task<AuthorWithBooksDto> GetAuthorBySlug(string slug, CancellationToken cancellationToken);
    Task<List<AuthorWithBooksDto>> GetAllAuthors(CancellationToken cancellationToken);
}
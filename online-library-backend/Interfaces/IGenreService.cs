namespace OnlineLibrary.Interfaces;

public interface IGenreService
{
    Task<GenreWithBooksDto> GetGenreBySlug(string slug, CancellationToken cancellationToken);
    Task<List<GenreWithBooksDto>> GetAllGenres(CancellationToken cancellationToken);
}
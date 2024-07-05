namespace OnlineLibrary.Interfaces;

public interface IBookService
{
    Task<BookWithGenresDto> GetBookBySlug(string slug, CancellationToken cancellationToken);
    Task<List<BookWithGenresDto>> GetAllBooks(CancellationToken cancellationToken);
}
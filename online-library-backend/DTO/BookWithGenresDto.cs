public record BookWithGenresDto(
    int id,
    string slug,
    string title,
    string? description,
    string coverUrl,
    AuthorDto author,
    List<GenreDto> genres);
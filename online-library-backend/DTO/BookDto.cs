public record BookDto(
    int id,
    string slug,
    string title,
    string? description,
    string coverUrl,
    AuthorDto author);
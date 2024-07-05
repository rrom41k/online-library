public record AuthorWithBooksDto(
    int id,
    string slug,
    string name,
    string surname,
    string? secondName,
    string? description,
    string country,
    string imageUrl,
    DateOnly? dateOfBirth,
    DateOnly? dateOfDeath,
    List<BookDto> books);
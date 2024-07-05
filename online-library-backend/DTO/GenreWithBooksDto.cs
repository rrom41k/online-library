public record GenreWithBooksDto(
    int id,
    string name,
    string slug,
    List<BookDto> books);
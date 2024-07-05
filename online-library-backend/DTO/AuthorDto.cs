public record AuthorDto(
    int id,
    string slug,
    string name,
    string surname,
    string? secondName,
    string? description,
    string country,
    string imageUrl,
    DateOnly? dateOfBirth,
    DateOnly? dateOfDeath);
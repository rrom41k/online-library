using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key] public int Id { get; set; }
    public string Slug { get; set; } // Slug
    public string Title { get; set; } // Название
    public string? Description { get; set; } // Краткое описание кники
    public string CoverUrl { get; set; } // Ссылка на обложку книги
    public int AuthorId { get; set; } // ID автора
    public Author Author { get; set; } // Ссылка на автора

    public ICollection<BookGenre> Genres { get; set; } =
        new HashSet<BookGenre>(); // Ссылка на книги, принадлежащие автору
}
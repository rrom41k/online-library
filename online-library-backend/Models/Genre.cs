using System.ComponentModel.DataAnnotations;

public class Genre
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } // название жанра
    public string Slug { get; set; } // Slug жанра 

    public ICollection<BookGenre> Books { get; set; } =
        new HashSet<BookGenre>(); // Ссылка на книги, принадлежащие жанру
}
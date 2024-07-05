using System.ComponentModel.DataAnnotations;

public class Author
{
    [Key] public int Id { get; set; }
    public string Slug { get; set; } // Slug
    public string? ImageUrl { get; set; } // ImageUrl
    public string Name { get; set; } // Имя
    public string Surname { get; set; } // Фамилия
    public string? SecondName { get; set; } // Отчество
    public string? Description { get; set; } // Краткое описание атора
    public string Country { get; set; } // Страна рождения
    public DateOnly? DateOfBirth { get; set; } // Дата рождения
    public DateOnly? DateOfDeath { get; set; } // Дата смерти
    public ICollection<Book> Books { get; set; } = new HashSet<Book>(); // Ссылка на книги, принадлежащие автору
}
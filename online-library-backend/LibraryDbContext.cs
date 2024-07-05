using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookGenre> BooksGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(e =>
        {
            e.HasKey(author => author.Id);
            e.Property(author => author.Id)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Genre>(e =>
        {
            e.HasKey(genre => genre.Id);
            e.Property(genre => genre.Id)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Book>(e =>
        {
            e.HasKey(book => book.Id);
            e.Property(book => book.Id)
                .ValueGeneratedOnAdd();
            e.HasOne(book => book.Author)
                .WithMany(author => author.Books);
        });

        // Многие ко мнгим книга жанры

        modelBuilder.Entity<BookGenre>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });

        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Book)
            .WithMany(b => b.Genres)
            .HasForeignKey(bg => bg.BookId);

        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(bg => bg.GenreId);
    }
}
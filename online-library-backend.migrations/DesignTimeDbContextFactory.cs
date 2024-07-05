using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
{
    public LibraryDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<LibraryDbContext>();

        var connectionString = configuration.GetConnectionString("Library");

        builder.UseMySql(connectionString,
            new MySqlServerVersion(new Version(8, 3, 0)),
            b =>
                b.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name));

        return new LibraryDbContext(builder.Options);
    }
}
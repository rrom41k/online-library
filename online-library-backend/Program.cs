using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Interfaces;
using OnlineLibrary.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    var connectionString = configuration.GetConnectionString("Library");

    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 3, 0)));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

CancellationTokenSource cancellation = new();
app.Lifetime.ApplicationStopping.Register(() => { cancellation.Cancel(); });

app.Run($"http://0.0.0.0:7140");
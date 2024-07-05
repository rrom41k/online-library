using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Interfaces;

namespace OnlineLibrary.Controllers;

[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET: api/genres/by-slug/:slug
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBookBySlug(string slug)
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        try
        {
            var genre = await _bookService.GetBookBySlug(slug, cancellationToken);

            if (genre == null) return NotFound();

            return Ok(genre);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Book not found.")
                return NotFound(ex.Message);

            return BadRequest(ex.Message);
        }
    }

    // GET: api/books/
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        try
        {
            var genres = await _bookService.GetAllBooks(cancellationToken);

            return Ok(genres);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
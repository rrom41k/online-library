using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Interfaces;

namespace OnlineLibrary.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // GET: api/authors/by-slug/:slug
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetAuthorBySlug(string slug)
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        try
        {
            var author = await _authorService.GetAuthorBySlug(slug, cancellationToken);

            if (author == null) return NotFound();

            return Ok(author);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Author not found.")
                return NotFound(ex.Message);

            return BadRequest(ex.Message);
        }
    }

    // GET: api/authors/
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        try
        {
            var authors = await _authorService.GetAllAuthors(cancellationToken);

            return Ok(authors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
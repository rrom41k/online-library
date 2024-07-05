using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Interfaces;

namespace OnlineLibrary.Controllers;

[Route("api/genres")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    // GET: api/genres/:slug
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetGenreBySlug(string slug)
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        try
        {
            var genre = await _genreService.GetGenreBySlug(slug, cancellationToken);

            if (genre == null) return NotFound();

            return Ok(genre);
        }
        catch (Exception ex)
        {
            if (ex.Message == "Genre not found.")
                return NotFound(ex.Message);

            return BadRequest(ex.Message);
        }
    }

    // GET: api/genres/
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        try
        {
            var genres = await _genreService.GetAllGenres(cancellationToken);

            return Ok(genres);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
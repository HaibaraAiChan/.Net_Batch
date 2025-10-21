using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Contracts.Services;

namespace MovieshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        [Route("top-grossing")]
        public async Task<IActionResult> Get30GHighestGrossingMovies()
        {
            var movies = await _movieService.Get30HighestGrossingMovies();
            if (movies == null || !movies.Any())
            {
                return NotFound("No movies found.");
            }
            return Ok(movies); // 200 OK
            // return NotFound(); // 404
            // return StatusCode(500); // 500
        }
        [HttpGet]
        [Route("id:{id}")]
        public async Task<IActionResult> GetMovieDetails(int id) 
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = "No movie Found" });
            }
            else 
            {
                return Ok(movie);
            }
        }
    }
}

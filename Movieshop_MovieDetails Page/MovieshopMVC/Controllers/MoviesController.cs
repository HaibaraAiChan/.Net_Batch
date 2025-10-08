using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieshopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Details(int id) //http://movieshop.com/movies/details
                                             //call the service method to get the movie details
        {
            var movieDetails = _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }
    }
}

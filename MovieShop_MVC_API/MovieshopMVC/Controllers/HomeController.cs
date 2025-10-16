using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieshopMVC.Models;
using System.Diagnostics;
using ApplicationCore.Models;

namespace MovieshopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMovieService _movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService =  movieService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var movieService = new Infrastructure.Services.MovieService();
            var movies = await _movieService.Get30HighestGrossingMovies();

            return View(movies);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieCard> Get30HighestGrossingMovies()
        {
            //Console.WriteLine($"hello ");
            //var movies = new List<MovieCard>()
            //{
            //    new MovieCard{ Id=1, Title="Avatar", PosterUrl="https://m.media-amazon.com/images/M/MV5BMjEwMjY3NjcxNF5BMl5BanBnXkFtZTcwODc5MTUwMw@@._V1_.jpg"},
            //    new MovieCard{ Id=2, Title="Avengers: Endgame", PosterUrl="https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzY4ODAzNzM@._V1_.jpg"},
            //    new MovieCard{ Id=3, Title="Titanic", PosterUrl="https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E3ZS00Y2JiLWEzNTEtODM1ZmJlZWIyMWFjXkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_.jpg"},
            //    new MovieCard{ Id=4, Title="Star Wars: The Force Awakens", PosterUrl="https://m.media-amazon.com/images/M/MV5BMjQ0MTgyNjAxMV5BMl5BanBnXkFtZTgwNjUzMDkyODE@._V1_.jpg"},
            //    new MovieCard{ Id=5, Title="Avengers: Infinity War", PosterUrl="https://m.media-amazon.com/images/M/MV5BMjMxNjY2MDU1OV5BMl5BanBnXkFtZTgwNzY1MTUwNTM@._V1_.jpg"},
            //    //new MovieCard{ Id=6, Title="Spider
            //};

            //return movies;
            var movies = _movieRepository.GetTop30GrossingMovies();

            var movieCards = new List<MovieCard>();
            
            foreach (var movie in movies)
            {
                var movieCard = new MovieCard
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                };
                movieCards.Add(movieCard);
            }

            return movieCards;
        }
        public MovieDetailModel GetMovieDetails(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null)
            {
                // handle movie not found, maybe throw an exception or return null
                return null;
            }
            var movieDetails = new MovieDetailModel
            {
                Id = movie.Id,
                Title = movie.Title,
                OverView = movie.OverView,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                Runtime = movie.Runtime,
                Price = movie.Price,
                Rating = movie.Rating,
                //Genres = movie.GenresOfMovie?.Select(g => g.Genre.Name).ToList(),
                Genres = movie.GenresOfMovie?.Select(g => new GenreModel
                {
                    Id = g.Genre.Id,
                    Name = g.Genre.Name
                }).ToList(),
                Casts = movie.MovieCasts?.Select(mc => new CastModel
                {
                    Id = mc.Cast.Id,
                    Name = mc.Cast.Name,
                    Character = mc.Character,
                    ProfilePath = mc.Cast.ProfilePath
                }).ToList(),
                Trailers = movie.Trailers?.Select(t => new TrailerModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    TrailerUrl = t.TrailerUrl
                }).ToList()
            };
            
           
            return movieDetails;
        }
    }
}

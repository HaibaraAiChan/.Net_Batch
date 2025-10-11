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
        public async Task<List<MovieCard>> Get30HighestGrossingMovies()
        {
            
            var movies = await _movieRepository.GetTop30GrossingMovies();

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
        public async Task<MovieDetailModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetById(id);
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

        public async Task<PagedResultSet<MovieCard>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);
            var movieCards = new List<MovieCard>();
            movieCards.AddRange(pagedMovies.Data.Select(m => new MovieCard{ Id=m.Id, Title=m.Title,PosterUrl=m.PosterUrl}));

            var pagedResultSet = new PagedResultSet<MovieCard>( movieCards, pageNumber, pagedMovies.PageSize, pagedMovies.Count);
                
            return pagedResultSet;
        }
    }
}

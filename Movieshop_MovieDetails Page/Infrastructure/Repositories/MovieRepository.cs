using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        // implement IMovieRepository methods here
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<Movie>> GetTop30GrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }
        public async Task<IEnumerable<Movie>> GetTop30RatedMovies()
        {
            throw new NotImplementedException();
        }
        public override async Task<Movie> GetById(int id)
        {
            //var movie = _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre)
            //                             .Include(m => m.Trailers)
            //                             .FirstOrDefault(m => m.Id == id);
            var movie = await _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre)
                                         .Include(m => m.Trailers)
                                         .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
                                         .Include(m => m.Trailers)
                                         .FirstOrDefaultAsync(m => m.Id == id);
            movie.Rating = await _dbContext.Reviews.Where(r => r.MovieId == id).AverageAsync(r => (decimal?)r.Rating) ?? 0;
            return movie;
        }

        public async Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var totalMoviesCountByGenre = await _dbContext.MovieGenres
                                              .Where(m => m.GenreId == genreId)
                                              .CountAsync();
            if (totalMoviesCountByGenre == 0)
            {
                throw new Exception("No movies found for the given genre");
            }

            var movies = await _dbContext.MovieGenres
                                         .Where(m => m.GenreId == genreId)// Filter by genreId
                                         .Include(m=>m.Movie) // Include the Movie entity
                                         .OrderBy(m => m.MovieId) // Order by MovieId to ensure consistent ordering
                                         .Skip((pageNumber - 1) * pageSize) // Skip the records for previous pages
                                         .Take(pageSize) // Take the number of records for the current page
                                         .Select(m => m.Movie) // Select only the Movie entity
                                         .ToListAsync(); // Execute the query and get the list of movies

            PagedResultSet<Movie> pagedMovies = new PagedResultSet<Movie>( movies, pageNumber, pageSize, totalMoviesCountByGenre);
            // var pagedResultSet = new PagedResultSet<Movie>(data, pageIndex, pageSize, count);
            return pagedMovies;
        }




    }
}

using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<Movie> GetTop30GrossingMovies()
        {
            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;
        }
        public IEnumerable<Movie> GetTop30RatedMovies()
        {
            throw new NotImplementedException();
        }
        public override Movie GetById(int id)
        {
            //var movie = _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre)
            //                             .Include(m => m.Trailers)
            //                             .FirstOrDefault(m => m.Id == id);
            var movie = _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre)
                                         .Include(m => m.Trailers)
                                         .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
                                         .Include(m => m.Trailers)
                                         .FirstOrDefault(m => m.Id == id);
            return movie;
        }
    }
}

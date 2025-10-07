using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities; 
using Infrastructure.Data;
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
    }
}

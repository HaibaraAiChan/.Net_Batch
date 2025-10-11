using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieServiceMock 
    //public class MovieServiceMock: IMovieService
    {
        public List<MovieCard> Get30HighestGrossingMovies() 
        {
            var movies = new List<MovieCard>()
            {
                new MovieCard{ Id=1, Title="Avatar", },
                new MovieCard{ Id=2, Title="Avengers: Endgame", },
                new MovieCard{ Id=3, Title="Titanic", },
                new MovieCard{ Id=4, Title="Star Wars: The Force Awakens", },
                new MovieCard{ Id=5, Title="Avengers: Infinity War", },
                //new MovieCard{ Id=6, Title="Spider
            };

            return movies;
        }
        public List<MovieCard> Get30HighestGrossingMoviesAsync()
        {
            var movies = new List<MovieCard>()
            {
                new MovieCard{ Id=1, Title="Avatar", },
                new MovieCard{ Id=2, Title="Avengers: Endgame", },
                new MovieCard{ Id=3, Title="Titanic", },
                new MovieCard{ Id=4, Title="Star Wars: The Force Awakens", },
                new MovieCard{ Id=5, Title="Avengers: Infinity War", },
                //new MovieCard{ Id=6, Title="Spider
            };
            return movies;
        }

        public MovieDetailModel GetMovieDetails(int id)
        {
            throw new NotImplementedException();
        }
    }

}

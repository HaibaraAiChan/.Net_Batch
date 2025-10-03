using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        public List<MovieCard> Get30HighestGrossingMovies()
        {
            var movies = new List<MovieCard>()
            {
                new MovieCard{ Id=1, Title="Avatar", PosterUrl="https://m.media-amazon.com/images/M/MV5BMjEwMjY3NjcxNF5BMl5BanBnXkFtZTcwODc5MTUwMw@@._V1_.jpg"},
                new MovieCard{ Id=2, Title="Avengers: Endgame", PosterUrl="https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzY4ODAzNzM@._V1_.jpg"},
                new MovieCard{ Id=3, Title="Titanic", PosterUrl="https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E3ZS00Y2JiLWEzNTEtODM1ZmJlZWIyMWFjXkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_.jpg"},
                new MovieCard{ Id=4, Title="Star Wars: The Force Awakens", PosterUrl="https://m.media-amazon.com/images/M/MV5BMjQ0MTgyNjAxMV5BMl5BanBnXkFtZTgwNjUzMDkyODE@._V1_.jpg"},
                new MovieCard{ Id=5, Title="Avengers: Infinity War", PosterUrl="https://m.media-amazon.com/images/M/MV5BMjMxNjY2MDU1OV5BMl5BanBnXkFtZTgwNzY1MTUwNTM@._V1_.jpg"},
                //new MovieCard{ Id=6, Title="Spider
            };

            return movies;
        }
    }
}

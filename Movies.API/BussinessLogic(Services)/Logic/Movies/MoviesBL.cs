using Movies.API.DataAccess_Repository_.Interfaces.Movies;
using Movies.API.DTOs.Movies;
/*
 
namespace Movies.API.BussinessLogic_Services_.Logic.Movies
{
    public class MoviesBL //: //IMoviesBL
    {
        private readonly IMoviesDA _MoviesDA;
     public MoviesBL(IMoviesDA moviesDA)
        {
            _MoviesDA = moviesDA;
        }
        
        public async Task<MovieDTO> AddMovie(CreateMovieDTO movie) {
            var newMovie = await _MoviesDA.AddMovie(movie);
            return new MovieDTO
            {
                Id = newMovie.Id,
                Title = newMovie.Title,
                Overview = newMovie.Overview,
                Release_Date = newMovie.Release_Date,
                Poster_Path = newMovie.Poster_Path,
                Backdrop_Path = newMovie.Backdrop_Path,
                Vote_Average = newMovie.Vote_Average
            };
                


        }
    }

}
*/
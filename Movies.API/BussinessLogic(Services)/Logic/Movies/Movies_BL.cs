using Movies.API.BussinessLogic_Services_.Interfaces.NewFolder;
using Movies.API.DataAccess_Repository_.Interfaces.Movies;
using Movies.API.DTOs.Movies;
using Movies.API.Models;

namespace Movies.API.BussinessLogic_Services_.Logic.Movies
{
    public class Movies_BL : I_Movies_BL
    {
        private readonly IMoviesDA _Movies;

        public Movies_BL(IMoviesDA movies)
        {
            _Movies = movies;

        }
        //create a new movie 
        public async Task<MovieDTO> AddMovie()
        {
            var movies = new MovieModel
            {
                Id = 0,
                Title = "string",
                Overview = "string",
                Release_Date = "string",
                Poster_Path = "string",
                Backdrop_Path = "string",
                Vote_Average = 0
            };

            var newMovies = await _Movies.AddMovies(movies);


            return new MovieDTO(
               newMovies.Id,
                newMovies.Title,
                newMovies.Overview,
                newMovies.Release_Date,
                newMovies.Poster_Path,
                newMovies.Backdrop_Path,
                newMovies.Vote_Average
                );


        }
        //update movie 
        public async Task<bool> UpdateMovie(int id)
        {
            var movie = await _Movies.GetMovieById(id);
            if (movie == null) return false;
            await _Movies.UpdateCategories(movie);
            return true;
        }

        //get the movies 
        public async Task<List<MovieDTO>> GetMovies()
        {
            var movies = await _Movies.GetMovies();
            return movies.Select(m => new MovieDTO(
                m.Id,
                m.Title,
                m.Overview,
                m.Release_Date,
                m.Poster_Path,
                m.Backdrop_Path,
                m.Vote_Average
                )).ToList();
        }

        //get movie y su  id
        public async Task<MovieDTO> GetMovieById(int id)
        {
            var movie = await _Movies.GetMovieById(id);
            if (movie == null) return null;
            return new MovieDTO(
                movie.Id,
                movie.Title,
                movie.Overview,
                movie.Release_Date,
                movie.Poster_Path,
                movie.Backdrop_Path,
                movie.Vote_Average
                );
        }

    
    }
}
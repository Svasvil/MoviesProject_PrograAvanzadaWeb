using Movies.API.Models;

namespace Movies.API.DataAccess_Repository_.Interfaces.Movies
{
    public interface IMoviesDA
    {
        Task<List<MovieModel>> GetMovies();
        Task<MovieModel> GetMovieById(int id);
        Task<MovieModel> AddMovies(MovieModel newmovie);
        Task<MovieModel> UpdateCategories(MovieModel updatedMovie);
    }
}

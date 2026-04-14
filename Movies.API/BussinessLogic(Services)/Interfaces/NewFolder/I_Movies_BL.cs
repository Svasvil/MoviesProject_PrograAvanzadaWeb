using Movies.API.DTOs.Movies;
using Movies.API.Models;

namespace Movies.API.BussinessLogic_Services_.Interfaces.NewFolder
{
    public interface I_Movies_BL
    {
        Task<List<MovieDTO>> GetMovies();

        Task<MovieDTO> GetMovieById(int id);
        Task<MovieDTO> AddMovie(MovieModel movie);
            Task<bool> UpdateMovie(int id);
                
    }
}

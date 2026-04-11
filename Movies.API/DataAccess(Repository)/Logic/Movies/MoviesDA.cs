using Microsoft.EntityFrameworkCore;
using Movies.API.DatabasesConnections;
using Movies.API.Models;


namespace Movies.API.DataAccess_Repository_.Logic
{
    public class MoviesDA : IMoviesDA
    {

        private readonly ObjContex _Contexto;
        public MoviesDA(ObjContex Contexto) => _Contexto = Contexto;

        //get all 
        public async Task<List<MovieModel>> GetMovies() =>
            await _Contexto.Movie.Select(m => new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Overview = m.Overview,
                Release_Date = m.Release_Date,
                Poster_Path = m.Poster_Path,
                Backdrop_Path = m.Backdrop_Path,
                Vote_Average = m.Vote_Average
            }).ToListAsync();

        //get id 
        public async Task<MovieModel> GetMovieById(int id)=>
            await _Contexto.Movie.Where(m => m.Id == id).Select(m => new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Overview = m.Overview,
                Release_Date = m.Release_Date,
                Poster_Path = m.Poster_Path,
                Backdrop_Path = m.Backdrop_Path,
                Vote_Average = m.Vote_Average
            }).FirstOrDefaultAsync();

        //agregar movie
        public async Task<MovieModel> AddMovies(MovieModel newmovie)
        {
            _Contexto.Movie.Add(newmovie);
            await _Contexto.SaveChangesAsync();
            return newmovie;
        }

        //Update para la categoria de peliculas.
        public async Task<MovieModel> UpdateCategories(MovieModel updatedMovie) { 
            _Contexto.Movie.Update(updatedMovie);
            await _Contexto.SaveChangesAsync();
            return updatedMovie;
        }

       
    }
}

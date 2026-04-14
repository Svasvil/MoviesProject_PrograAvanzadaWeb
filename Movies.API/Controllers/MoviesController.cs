using Microsoft.AspNetCore.Mvc;
using Movies.API.Models;

namespace Movies.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
      private readonly
            
            BussinessLogic_Services_.Interfaces.NewFolder.I_Movies_BL _moviesBL;
        public MoviesController(BussinessLogic_Services_.Interfaces.NewFolder.I_Movies_BL moviesBL)
        {
            _moviesBL = moviesBL;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _moviesBL.GetMovies();
            return Ok(movies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _moviesBL.GetMovieById(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieModel movie)
        {
            if (movie == null) return BadRequest();

     
          

            var newMovie = await _moviesBL.AddMovie(movie);

            return Ok(newMovie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id)
        {
            var result = await _moviesBL.UpdateMovie(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

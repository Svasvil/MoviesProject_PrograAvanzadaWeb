namespace Movies.API.DTOs.Movies
{
    public record CreateMovieDTO
    (
         int Id,
         string Title,
         string Overview,
         string Release_Date,
         string Poster_Path,
         string Backdrop_Path,
         double Vote_Average
    );
}

namespace Movies.API.Models
{
    public class MovieModel
    {
        //Estos datos son los que agarre con un get de la APi de The Movie DB, por eso se llaman asi, para que se mapeen bien
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Release_Date { get; set; }
        public string Poster_Path { get; set; }
        public string Backdrop_Path { get; set; }
        public double Vote_Average { get; set; }
    }
}
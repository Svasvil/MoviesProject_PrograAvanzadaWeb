using Moq;
using Movies.API.BussinessLogic_Services_.Logic.Movies;
using Movies.API.DataAccess_Repository_.Interfaces.Movies;
using Movies.API.Models;

namespace Movies.API.Tests
{
    public class MoviesBLTests
    {
        private readonly Mock<IMoviesDA> _moviesDAMock;
        private readonly Movies_BL _moviesBL;

        public MoviesBLTests()
        {
            _moviesDAMock = new Mock<IMoviesDA>();
            _moviesBL = new Movies_BL(_moviesDAMock.Object);
        }

        [Fact]
        public async Task GetMovies_DeberiaRetornarListaConDatos()
        {
            // Arrange
            var movies = new List<MovieModel>
            {
                new MovieModel
                {
                    Id = 1,
                    Title = "Inception",
                    Overview = "Sueños dentro de sueños",
                    Release_Date = "2010-07-16",
                    Poster_Path = "/poster1.jpg",
                    Backdrop_Path = "/backdrop1.jpg",
                    Vote_Average = 8.8
                },
                new MovieModel
                {
                    Id = 2,
                    Title = "Interstellar",
                    Overview = "Viaje espacial",
                    Release_Date = "2014-11-07",
                    Poster_Path = "/poster2.jpg",
                    Backdrop_Path = "/backdrop2.jpg",
                    Vote_Average = 8.6
                }
            };

            _moviesDAMock.Setup(x => x.GetMovies()).ReturnsAsync(movies);

            // Act
            var result = await _moviesBL.GetMovies();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Inception", result[0].Title);
            Assert.Equal("Interstellar", result[1].Title);
        }

        [Fact]
        public async Task GetMovies_SiNoHayPeliculas_DeberiaRetornarListaVacia()
        {
            // Arrange
            _moviesDAMock.Setup(x => x.GetMovies()).ReturnsAsync(new List<MovieModel>());

            // Act
            var result = await _moviesBL.GetMovies();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetMovieById_SiExiste_DeberiaRetornarMovieDTO()
        {
            // Arrange
            var movie = new MovieModel
            {
                Id = 1,
                Title = "Titanic",
                Overview = "Historia de amor",
                Release_Date = "1997-12-19",
                Poster_Path = "/poster.jpg",
                Backdrop_Path = "/backdrop.jpg",
                Vote_Average = 7.9
            };

            _moviesDAMock.Setup(x => x.GetMovieById(1)).ReturnsAsync(movie);

            // Act
            var result = await _moviesBL.GetMovieById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Titanic", result.Title);
            Assert.Equal("Historia de amor", result.Overview);
        }

        [Fact]
        public async Task GetMovieById_SiNoExiste_DeberiaRetornarNull()
        {
            // Arrange
            _moviesDAMock.Setup(x => x.GetMovieById(99)).ReturnsAsync((MovieModel)null);

            // Act
            var result = await _moviesBL.GetMovieById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateMovie_SiExiste_DeberiaRetornarTrue()
        {
            // Arrange
            var movie = new MovieModel
            {
                Id = 1,
                Title = "Avatar",
                Overview = "Pandora",
                Release_Date = "2009-12-18",
                Poster_Path = "/poster.jpg",
                Backdrop_Path = "/backdrop.jpg",
                Vote_Average = 7.8
            };

            _moviesDAMock.Setup(x => x.GetMovieById(1)).ReturnsAsync(movie);
            _moviesDAMock.Setup(x => x.UpdateCategories(movie)).ReturnsAsync(movie);

            // Act
            var result = await _moviesBL.UpdateMovie(1);

            // Assert
            Assert.True(result);
            _moviesDAMock.Verify(x => x.UpdateCategories(movie), Times.Once);
        }

        [Fact]
        public async Task UpdateMovie_SiNoExiste_DeberiaRetornarFalse()
        {
            // Arrange
            _moviesDAMock.Setup(x => x.GetMovieById(50)).ReturnsAsync((MovieModel)null);

            // Act
            var result = await _moviesBL.UpdateMovie(50);

            // Assert
            Assert.False(result);
            _moviesDAMock.Verify(x => x.UpdateCategories(It.IsAny<MovieModel>()), Times.Never);
        }
    }
}

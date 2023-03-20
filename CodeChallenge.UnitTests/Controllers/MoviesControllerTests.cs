using CodeChallenge.Controllers;
using CodeChallenge.IServices;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeChallenge.UnitTests.Controllers
{
    public class MoviesControllerTests
    {
        private readonly MoviesController _moviesController;
        private readonly Mock<IMovieService> _movieServicMock;

        public MoviesControllerTests()
        {
            _movieServicMock = new Mock<IMovieService>();
            _moviesController = new MoviesController(_movieServicMock.Object);
        }

        [Fact]
        public void Constructor_NullMovieService_ThrowException()
        {
            Action act = () => new MoviesController(null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetMoviesAll_ReturnsOk()
        {
            var foundMovie = new Movie_DTO();
            var foundMovies = new List<Movie_DTO>() { foundMovie };
            var expectedResult = new Movie_DTO();
            var expectedResults = new List<Movie_DTO>() { expectedResult };

            _movieServicMock.Setup(exp => exp.GetMoviesAll()).Returns(Task.FromResult(foundMovies.AsEnumerable()));
            var actionResult = await _moviesController.GetMoviesAll();

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<List<Movie_DTO>>();
            response.Should().BeEquivalentTo(expectedResults);
        }

        [Fact]
        public async Task GetMovieById_WithMovieIdCorrect_ReturnsOk()
        {
            Guid MovieId = Guid.NewGuid();

            var Movie = new Movie_DTO
            {
                Uuid = MovieId,
                Title = "Phuc",
                ReleaseDate = DateTime.UtcNow,
                Rating = 5
            };

            _movieServicMock.Setup(exp => exp.GetMovieById(MovieId)).Returns(Task.FromResult(Movie));
            var actionResult = await _moviesController.GetMovieById(MovieId);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<Movie_DTO>();
            response.Should().BeEquivalentTo(Movie);
        }

        [Fact]
        public async Task GetMovieById_WithMovieIdNotCorrect_ReturnsNotFound()
        {
            Guid MovieId = Guid.NewGuid();

            Movie_DTO? Movie = null;

            _movieServicMock.Setup(exp => exp.GetMovieById(MovieId)).Returns(Task.FromResult(Movie));
            var actionResult = await _moviesController.GetMovieById(MovieId);

            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateMovie_ReturnsOk()
        {
            var addMovieViewModel = new AddMovieViewModel
            {
                Title = "Phuc",
                ReleaseDate = DateTime.UtcNow,
                Rating = 5
            };

            var Movie = new Movie_DTO
            {
                Uuid = Guid.NewGuid(),
                Title = addMovieViewModel.Title,
                ReleaseDate = addMovieViewModel.ReleaseDate,
                Rating = addMovieViewModel.Rating
            };

            _movieServicMock.Setup(exp => exp.CreateMovie(addMovieViewModel)).Returns(Task.FromResult(Movie));
            var actionResult = await _moviesController.CreateMovie(addMovieViewModel);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<Movie_DTO>();
            response.Should().BeEquivalentTo(Movie);
        }

        [Fact]
        public async Task CreateMovie_ReturnsNotFound()
        {
            var addMovieViewModel = new AddMovieViewModel
            {
                Title = "Phuc",
                ReleaseDate = DateTime.UtcNow,
                Rating = 5
            };

            Movie_DTO? Movie = null;

            _movieServicMock.Setup(exp => exp.CreateMovie(addMovieViewModel)).Returns(Task.FromResult(Movie));
            var actionResult = await _moviesController.CreateMovie(addMovieViewModel);

            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateMovie_ReturnsTrue()
        {
            var MovieId = Guid.NewGuid();

            var updateMovieViewModel = new UpdateMovieViewModel
            {
                Title = "Phuc",
                ReleaseDate = DateTime.UtcNow,
                Rating = 5
            };

            var result = true;

            _movieServicMock.Setup(exp => exp.UpdateMovie(MovieId, updateMovieViewModel)).Returns(Task.FromResult(result));
            var actionResult = await _moviesController.UpdateMovie(MovieId, updateMovieViewModel);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task UpdateMovie_ReturnsFalse()
        {
            var MovieId = Guid.NewGuid();

            var updateMovieViewModel = new UpdateMovieViewModel
            {
                Title = "Phuc",
                ReleaseDate = DateTime.UtcNow,
                Rating = 5
            };

            var result = false;

            _movieServicMock.Setup(exp => exp.UpdateMovie(MovieId, updateMovieViewModel)).Returns(Task.FromResult(result));
            var actionResult = await _moviesController.UpdateMovie(MovieId, updateMovieViewModel);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task DeleteMovie_ReturnsTrue()
        {
            var MovieId = Guid.NewGuid();


            var result = true;

            _movieServicMock.Setup(exp => exp.DeleteMovie(MovieId)).Returns(Task.FromResult(result));
            var actionResult = await _moviesController.DeleteMovie(MovieId);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task DeleteMovie_ReturnsFalse()
        {
            var MovieId = Guid.NewGuid();


            var result = false;

            _movieServicMock.Setup(exp => exp.DeleteMovie(MovieId)).Returns(Task.FromResult(result));
            var actionResult = await _moviesController.DeleteMovie(MovieId);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
        }
    }
}

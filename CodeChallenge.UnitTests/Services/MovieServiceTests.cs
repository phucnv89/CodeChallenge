using CodeChallenge.DAL.Interfaces;
using CodeChallenge.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeChallenge.UnitTests.Services
{
    public class MovieServiceTests
    {
        private readonly MovieService _movieService;
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly Mock<ILogger<MovieService>> _loggerMock;

        public MovieServiceTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _loggerMock = new Mock<ILogger<MovieService>>();
            _movieService = new MovieService(_loggerMock.Object, _movieRepositoryMock.Object);
        }

        [Fact]
        public void Constructor_NullMovieService_ThrowException()
        {
            Action act = () => new MovieService(_loggerMock.Object, null);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}

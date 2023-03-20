using CodeChallenge.DAL.Interfaces;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;
using CodeChallenge.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
    public class DirectorServiceTests
    {
        private readonly DirectorService _directorService;
        private readonly Mock<IDirectorRepository> _directorRepositoryMock;
        private readonly Mock<ILogger<DirectorService>> _loggerMock;

        public DirectorServiceTests()
        {
            _directorRepositoryMock = new Mock<IDirectorRepository>();
            _loggerMock = new Mock<ILogger<DirectorService>>();
            _directorService = new DirectorService(_loggerMock.Object, _directorRepositoryMock.Object);
        }

        [Fact]
        public void Constructor_NullDirectorService_ThrowException()
        {
            Action act = () => new DirectorService(_loggerMock.Object, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetDirectorsAll_ReturnsOk()
        {
            var foundDirector = new Director_DTO();
            var foundDirectors = new List<Director_DTO>() { foundDirector };
            var expectedResult = new Director_DTO();
            var expectedResults = new List<Director_DTO>() { expectedResult };

            _directorRepositoryMock.Setup(exp => exp.GetDirectorsAll()).Returns(Task.FromResult(foundDirectors.AsEnumerable()));
            var actionResult = await _directorService.GetDirectorsAll();

            actionResult.Should().BeOfType<List<Director_DTO>>();
            actionResult.Should().BeEquivalentTo(expectedResults);
        }

        [Fact]
        public async Task GetDirectorById_WithDirectorIdCorrect_ReturnsOk()
        {
            Guid directorId = Guid.NewGuid();

            var director = new Director_DTO
            {
                Uuid = directorId,
                Name = "Phuc",
                Birthdate = DateTime.UtcNow
            };

            _directorRepositoryMock.Setup(exp => exp.GetDirectorById(directorId)).Returns(Task.FromResult(director));
            var actionResult = await _directorService.GetDirectorById(directorId);

            actionResult.Should().BeOfType<Director_DTO>();
            actionResult.Should().BeEquivalentTo(director);
        }

        [Fact]
        public async Task GetDirectorById_WithDirectorIdNotCorrect_ReturnsNull()
        {
            Guid directorId = Guid.NewGuid();

            Director_DTO? director = null;

            _directorRepositoryMock.Setup(exp => exp.GetDirectorById(directorId)).Returns(Task.FromResult(director));
            var actionResult = await _directorService.GetDirectorById(directorId);

            actionResult.Should().BeEquivalentTo(director);
        }

        [Fact]
        public async Task CreateDirector_ReturnsOk()
        {
            var addDirectorViewModel = new AddDirectorViewModel
            {
                Name = "Phuc",
                Birthdate = DateTime.UtcNow
            };

            var director = new Director_DTO
            {
                Uuid = Guid.NewGuid(),
                Name = addDirectorViewModel.Name,
                Birthdate = addDirectorViewModel.Birthdate
            };

            _directorRepositoryMock.Setup(exp => exp.CreateDirector(addDirectorViewModel)).Returns(Task.FromResult(director));
            var actionResult = await _directorService.CreateDirector(addDirectorViewModel);

            actionResult.Should().BeOfType<Director_DTO>();
            actionResult.Should().BeEquivalentTo(director);
        }


        [Fact]
        public async Task CreateDirector_ReturnsNull()
        {
            var addDirectorViewModel = new AddDirectorViewModel
            {
                Name = "Phuc",
                Birthdate = DateTime.UtcNow
            };

            Director_DTO? director = null;

            _directorRepositoryMock.Setup(exp => exp.CreateDirector(addDirectorViewModel)).Returns(Task.FromResult(director));
            var actionResult = await _directorService.CreateDirector(addDirectorViewModel);

            actionResult.Should().BeEquivalentTo(director);
        }

        [Fact]
        public async Task UpdateDirector_ReturnsTrue()
        {
            var directorId = Guid.NewGuid();

            var updateDirectorViewModel = new UpdateDirectorViewModel
            {
                Name = "Phuc",
                Birthdate = DateTime.UtcNow
            };

            var result = true;

            _directorRepositoryMock.Setup(exp => exp.UpdateDirector(directorId, updateDirectorViewModel)).Returns(Task.FromResult(result));
            var actionResult = await _directorService.UpdateDirector(directorId, updateDirectorViewModel);

            actionResult.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateDirector_ReturnsFalse()
        {
            var directorId = Guid.NewGuid();

            var updateDirectorViewModel = new UpdateDirectorViewModel
            {
                Name = "Phuc",
                Birthdate = DateTime.UtcNow
            };

            var result = false;

            _directorRepositoryMock.Setup(exp => exp.UpdateDirector(directorId, updateDirectorViewModel)).Returns(Task.FromResult(result));
            var actionResult = await _directorService.UpdateDirector(directorId, updateDirectorViewModel);

            actionResult.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteDirector_ReturnsTrue()
        {
            var directorId = Guid.NewGuid();


            var result = true;

            _directorRepositoryMock.Setup(exp => exp.DeleteDirector(directorId)).Returns(Task.FromResult(result));
            var actionResult = await _directorService.DeleteDirector(directorId);


            actionResult.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteDirector_ReturnsFalse()
        {
            var directorId = Guid.NewGuid();


            var result = false;

            _directorRepositoryMock.Setup(exp => exp.DeleteDirector(directorId)).Returns(Task.FromResult(result));
            var actionResult = await _directorService.DeleteDirector(directorId);

            actionResult.Should().BeFalse();
        }
    }
}

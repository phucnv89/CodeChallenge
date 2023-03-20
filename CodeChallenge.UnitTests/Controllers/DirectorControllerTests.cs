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
    public class DirectorControllerTests
    {
        private readonly DirectorController _directorController;
        private readonly Mock<IDirectorService> _directorServiceMock;

        public DirectorControllerTests()
        {
            _directorServiceMock = new Mock<IDirectorService>();
            _directorController = new DirectorController(_directorServiceMock.Object);
        }

        [Fact]
        public void Constructor_NullDirectorService_ThrowException()
        {
            Action act = () => new DirectorController(null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetDirectorsAll_ReturnsOk()
        {
            var foundDirector = new Director_DTO();
            var foundDirectors = new List<Director_DTO>() { foundDirector };
            var expectedResult = new Director_DTO();
            var expectedResults = new List<Director_DTO>() { expectedResult };

            _directorServiceMock.Setup(exp => exp.GetDirectorsAll()).Returns(Task.FromResult(foundDirectors.AsEnumerable()));
            var actionResult = await _directorController.GetDirectorsAll();

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<List<Director_DTO>>();
            response.Should().BeEquivalentTo(expectedResults);
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

            _directorServiceMock.Setup(exp => exp.GetDirectorById(directorId)).Returns(Task.FromResult(director));
            var actionResult = await _directorController.GetDirectorById(directorId);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<Director_DTO>();
            response.Should().BeEquivalentTo(director);
        }

        [Fact]
        public async Task GetDirectorById_WithDirectorIdNotCorrect_ReturnsNotFound()
        {
            Guid directorId = Guid.NewGuid();

            Director_DTO? director = null;

            _directorServiceMock.Setup(exp => exp.GetDirectorById(directorId)).Returns(Task.FromResult(director));
            var actionResult = await _directorController.GetDirectorById(directorId);

            actionResult.Should().BeOfType<NotFoundResult>();
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

            _directorServiceMock.Setup(exp => exp.CreateDirector(addDirectorViewModel)).Returns(Task.FromResult(director));
            var actionResult = await _directorController.CreateDirector(addDirectorViewModel);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<Director_DTO>();
            response.Should().BeEquivalentTo(director);
        }

        [Fact]
        public async Task CreateDirector_ReturnsNotFound()
        {
            var addDirectorViewModel = new AddDirectorViewModel
            {
                Name = "Phuc",
                Birthdate = DateTime.UtcNow
            };

            Director_DTO? director = null;

            _directorServiceMock.Setup(exp => exp.CreateDirector(addDirectorViewModel)).Returns(Task.FromResult(director));
            var actionResult = await _directorController.CreateDirector(addDirectorViewModel);

            actionResult.Should().BeOfType<NotFoundResult>();
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

            _directorServiceMock.Setup(exp => exp.UpdateDirector(directorId, updateDirectorViewModel)).Returns(Task.FromResult(result));
            var actionResult = await _directorController.UpdateDirector(directorId, updateDirectorViewModel);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
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

            _directorServiceMock.Setup(exp => exp.UpdateDirector(directorId, updateDirectorViewModel)).Returns(Task.FromResult(result));
            var actionResult = await _directorController.UpdateDirector(directorId, updateDirectorViewModel);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task DeleteDirector_ReturnsTrue()
        {
            var directorId = Guid.NewGuid();


            var result = true;

            _directorServiceMock.Setup(exp => exp.DeleteDirector(directorId)).Returns(Task.FromResult(result));
            var actionResult = await _directorController.DeleteDirector(directorId);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task DeleteDirector_ReturnsFalse()
        {
            var directorId = Guid.NewGuid();


            var result = false;

            _directorServiceMock.Setup(exp => exp.DeleteDirector(directorId)).Returns(Task.FromResult(result));
            var actionResult = await _directorController.DeleteDirector(directorId);

            actionResult.Should().BeOfType<OkObjectResult>();
            var response = ((OkObjectResult)actionResult).Value;
            response.Should().BeOfType<bool>();
            response.Should().BeEquivalentTo(result);
        }
    }
}

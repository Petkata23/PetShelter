using Moq;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Tests.Services
{
    public class LocationsServiceTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock = new Mock<ILocationRepository>();

        private readonly ILocationsService _service;

        public LocationsServiceTests()
        {
            _service = new LocationsService(_locationRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var locationDto = new LocationDto()
            {
                Id = 0,
                City = "City",
                Address = "Address",
                Country = "Country"
            };

            // Act
            await _service.SaveAsync(locationDto);

            //Assert
            _locationRepositoryMock.Verify(x => x.SaveAsync(locationDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _locationRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _locationRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidBreedId_ThenReturnUser(int locationId)
        {
            // Arrange
            var locationDto = new LocationDto()
            {
                Id = 0,
                City = "City",
                Address = "Address",
                Country = "Country"
            };

            _locationRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(locationId))))
                .ReturnsAsync(locationDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(locationId);

            //Assert
            _locationRepositoryMock.Verify(x => x.GetByIdAsync(locationId), Times.Once);
            Assert.That(userResult == locationDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidBreedId_ThenReturnDefault(int locationId)
        {
            //Arrange
            var location = (LocationDto)default;

            _locationRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(locationId))))
                .ReturnsAsync(location);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(locationId);

            //Assert
            _locationRepositoryMock.Verify(x => x.GetByIdAsync(locationId), Times.Once);
            Assert.That(userResult == location);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var locationDto = new LocationDto()
            {
                Id = 0,
                City = "City",
                Address = "Address",
                Country = "Country"
            };

            _locationRepositoryMock.Setup(s => s.SaveAsync(It.Is<LocationDto>(x => x.Equals(locationDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(locationDto);

            //Assert
            _locationRepositoryMock.Verify(x => x.SaveAsync(locationDto), Times.Once);
        }

    }
}

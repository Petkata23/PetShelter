using Moq;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Tests.Services
{
    public class SheltersServiceTests
    {
        private readonly Mock<IShelterRepository> _shelterRepositoryMock = new Mock<IShelterRepository>();

        private readonly ISheltersService _service;

        public SheltersServiceTests()
        {
            _service = new SheltersService(_shelterRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            var shelterDto = new ShelterDto
            {
                PetCapacity = 10,
                LocationId = 1,
                Employees = new List<UserDto>(),
                Pets = new List<PetDto>()
            };

            // Act
            await _service.SaveAsync(shelterDto);

            //Assert
            _shelterRepositoryMock.Verify(x => x.SaveAsync(shelterDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _shelterRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _shelterRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidShelterId_ThenReturnUser(int shelterId)
        {
            // Arrange
            var shelterDto = new ShelterDto
            {
                PetCapacity = 10,
                LocationId = 1,
                Employees = new List<UserDto>(),
                Pets = new List<PetDto>()
            };

            _shelterRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(shelterId))))
                .ReturnsAsync(shelterDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(shelterId);

            //Assert
            _shelterRepositoryMock.Verify(x => x.GetByIdAsync(shelterId), Times.Once);
            Assert.That(userResult == shelterDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidShelterId_ThenReturnDefault(int shelterId)
        {
            //Arrange
            var role = (ShelterDto)default;

            _shelterRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(shelterId))))
                .ReturnsAsync(role);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(shelterId);

            //Assert
            _shelterRepositoryMock.Verify(x => x.GetByIdAsync(shelterId), Times.Once);
            Assert.That(userResult == role);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var shelterDto = new ShelterDto
            {
                PetCapacity = 10,
                LocationId = 1,
                Employees = new List<UserDto>(),
                Pets = new List<PetDto>()
            };

            _shelterRepositoryMock.Setup(s => s.SaveAsync(It.Is<ShelterDto>(x => x.Equals(shelterDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(shelterDto);

            //Assert
            _shelterRepositoryMock.Verify(x => x.SaveAsync(shelterDto), Times.Once);
        }

        [Test]
        public async Task WhenGetAllAsync_ThenReturnAllRoles()
        {
            // Arrange
            var shelters = new List<ShelterDto>
            {
                new ShelterDto { PetCapacity = 10, LocationId = 1, Employees = new List<UserDto>(), Pets = new List<PetDto>() },
                new ShelterDto { PetCapacity = 15, LocationId = 2, Employees = new List<UserDto>(), Pets = new List<PetDto>() }
            };

            _shelterRepositoryMock.Setup(p => p.GetAllAsync()).ReturnsAsync(shelters);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _shelterRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(shelters));
        }
    }
}

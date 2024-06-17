using Moq;
using PetShelter.Data.Entities;
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
    public class PetVaccinesServiceTests
    {
        private readonly Mock<IPetVaccineRepository> _petVaccineRepositoryMock = new Mock<IPetVaccineRepository>();

        private readonly IPetVaccinesService _service;

        public PetVaccinesServiceTests()
        {
            _service = new PetVaccinesService(_petVaccineRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            var petVaccineDto = new PetVaccineDto
            {
                PetId = 1,
                VaccineId = 1
            };

            // Act
            await _service.SaveAsync(petVaccineDto);

            //Assert
            _petVaccineRepositoryMock.Verify(x => x.SaveAsync(petVaccineDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _petVaccineRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _petVaccineRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidPetVaccineId_ThenReturnUser(int petVaccineId)
        {
            // Arrange
            var petVaccineDto = new PetVaccineDto
            {
                PetId = 1,
                VaccineId = 1
            };

            _petVaccineRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(petVaccineId))))
                .ReturnsAsync(petVaccineDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(petVaccineId);

            //Assert
            _petVaccineRepositoryMock.Verify(x => x.GetByIdAsync(petVaccineId), Times.Once);
            Assert.That(userResult == petVaccineDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidPetVaccineId_ThenReturnDefault(int petVaccineId)
        {
            //Arrange
            var petVaccine = (PetVaccineDto)default;

            _petVaccineRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(petVaccineId))))
                .ReturnsAsync(petVaccine);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(petVaccineId);

            //Assert
            _petVaccineRepositoryMock.Verify(x => x.GetByIdAsync(petVaccineId), Times.Once);
            Assert.That(userResult == petVaccine);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var petVaccineDto = new PetVaccineDto
            {
                PetId = 1,
                VaccineId = 1
            };

            _petVaccineRepositoryMock.Setup(s => s.SaveAsync(It.Is<PetVaccineDto>(x => x.Equals(petVaccineDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(petVaccineDto);

            //Assert
            _petVaccineRepositoryMock.Verify(x => x.SaveAsync(petVaccineDto), Times.Once);
        }
    }
}

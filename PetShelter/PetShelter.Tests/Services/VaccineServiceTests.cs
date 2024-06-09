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
    public class VaccineServiceTests
    {
        private readonly Mock<IVaccineRepository> _vaccineRepositoryMock = new Mock<IVaccineRepository>();

        private readonly IVaccinesService _service;

        public VaccineServiceTests()
        {
            _service = new VaccinesService(_vaccineRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            var vaccineDto = new VaccineDto
            {
                Name = "VaccineName",
                Description = "VaccineDescription",
                PetVaccines = new List<PetVaccineDto>()
            };


            // Act
            await _service.SaveAsync(vaccineDto);

            //Assert
            _vaccineRepositoryMock.Verify(x => x.SaveAsync(vaccineDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _vaccineRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _vaccineRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidVaccineId_ThenReturnUser(int vaccineId)
        {
            // Arrange
            var vaccineDto = new VaccineDto
            {
                Name = "VaccineName",
                Description = "VaccineDescription",
                PetVaccines = new List<PetVaccineDto>()
            };

            _vaccineRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(vaccineId))))
                .ReturnsAsync(vaccineDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(vaccineId);

            //Assert
            _vaccineRepositoryMock.Verify(x => x.GetByIdAsync(vaccineId), Times.Once);
            Assert.That(userResult == vaccineDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidVaccineId_ThenReturnDefault(int vaccineId)
        {
            //Arrange
            var vaccine = (VaccineDto)default;

            _vaccineRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(vaccineId))))
                .ReturnsAsync(vaccine);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(vaccineId);

            //Assert
            _vaccineRepositoryMock.Verify(x => x.GetByIdAsync(vaccineId), Times.Once);
            Assert.That(userResult == vaccine);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var vaccineDto = new VaccineDto
            {
                Name = "VaccineName",
                Description = "VaccineDescription",
                PetVaccines = new List<PetVaccineDto>()
            };

            _vaccineRepositoryMock.Setup(s => s.SaveAsync(It.Is<VaccineDto>(x => x.Equals(vaccineDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(vaccineDto);

            //Assert
            _vaccineRepositoryMock.Verify(x => x.SaveAsync(vaccineDto), Times.Once);
        }
    }
}

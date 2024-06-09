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
    public class PetsServiceTests
    {
        private readonly Mock<IPetRepository> _petRepositoryMock = new Mock<IPetRepository>();
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IShelterRepository> _shelterRepositoryMock = new Mock<IShelterRepository>();

        private readonly IPetsService _service;

        public PetsServiceTests()
        {
            _service = new PetsService(_petRepositoryMock.Object, _userRepositoryMock.Object, _shelterRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var petDto = new PetDto()
            {
                Id = 0,
                Name = "PetName",
                Age = 2,
                Color = "PetColor",
                IsAdopted = false,
                IsEuthanized = false,
                PetTypeId = 1, 
                BreedId = 1,
                AdopterId = null, 
                GiverId = null, 
                ShelterId = null, 
                PetVaccines = new List<PetVaccineDto>() 
            };

            // Act
            await _service.SaveAsync(petDto);

            //Assert
            _petRepositoryMock.Verify(x => x.SaveAsync(petDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _petRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _petRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidPetId_ThenReturnUser(int petId)
        {
            // Arrange
            var petDto = new PetDto()
            {
                Id = 0,
                Name = "PetName",
                Age = 2,
                Color = "PetColor",
                IsAdopted = false,
                IsEuthanized = false,
                PetTypeId = 1,
                BreedId = 1,
                AdopterId = null,
                GiverId = null,
                ShelterId = null,
                PetVaccines = new List<PetVaccineDto>()
            };

            _petRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(petId))))
                .ReturnsAsync(petDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(petId);

            //Assert
            _petRepositoryMock.Verify(x => x.GetByIdAsync(petId), Times.Once);
            Assert.That(userResult == petDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidPetId_ThenReturnDefault(int petId)
        {
            //Arrange
            var pet = (PetDto)default;

            _petRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(petId))))
                .ReturnsAsync(pet);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(petId);

            //Assert
            _petRepositoryMock.Verify(x => x.GetByIdAsync(petId), Times.Once);
            Assert.That(userResult == pet);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var petDto = new PetDto()
            {
                Id = 0,
                Name = "PetName",
                Age = 2,
                Color = "PetColor",
                IsAdopted = false,
                IsEuthanized = false,
                PetTypeId = 1,
                BreedId = 1,
                AdopterId = null,
                GiverId = null,
                ShelterId = null,
                PetVaccines = new List<PetVaccineDto>()
            };

            _petRepositoryMock.Setup(s => s.SaveAsync(It.Is<PetDto>(x => x.Equals(petDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(petDto);

            //Assert
            _petRepositoryMock.Verify(x => x.SaveAsync(petDto), Times.Once);
        }
    }
}

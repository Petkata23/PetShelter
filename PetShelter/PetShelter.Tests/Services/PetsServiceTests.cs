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
    public class PetsServiceTests
    {
        private Mock<IPetRepository> _petRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IShelterRepository> _shelterRepositoryMock;
        private Mock<IVaccineRepository> _vaccineRepositoryMock;
        private Mock<IPetVaccineRepository> _petVaccineRepositoryMock;

        private IPetsService _service;

        [SetUp]
        public void SetUp()
        {
            _petRepositoryMock = new Mock<IPetRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _shelterRepositoryMock = new Mock<IShelterRepository>();
            _vaccineRepositoryMock = new Mock<IVaccineRepository>();
            _petVaccineRepositoryMock = new Mock<IPetVaccineRepository>();
            _service = new PetsService(
                _petRepositoryMock.Object,
                _userRepositoryMock.Object,
                _shelterRepositoryMock.Object,
                _vaccineRepositoryMock.Object,
                _petVaccineRepositoryMock.Object
            );
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

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidPetId_ThenReturnPet(int petId)
        {
            // Arrange
            var petDto = new PetDto()
            {
                Id = petId,
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

            // Act
            var petResult = await _service.GetByIdIfExistsAsync(petId);

            // Assert
            _petRepositoryMock.Verify(x => x.GetByIdAsync(petId), Times.Once);
            Assert.That(petResult, Is.EqualTo(petDto));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidPetId_ThenReturnDefault(int petId)
        {
            // Arrange
            var pet = (PetDto)default;

            _petRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(petId))))
                .ReturnsAsync(pet);

            // Act
            var petResult = await _service.GetByIdIfExistsAsync(petId);

            // Assert
            _petRepositoryMock.Verify(x => x.GetByIdAsync(petId), Times.Once);
            Assert.That(petResult, Is.EqualTo(pet));
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

            // Act
            await _service.SaveAsync(petDto);

            // Assert
            _petRepositoryMock.Verify(x => x.SaveAsync(petDto), Times.Once);
        }

        [Test]
        public async Task WhenGivePetAsync_WithValidData_ThenCreateAsync()
        {
            // Arrange
            int userId = 1;
            int shelterId = 1;
            var petDto = new PetDto()
            {
                Name = "PetName",
                Age = 2,
                Color = "PetColor",
                IsAdopted = false,
                IsEuthanized = false,
                PetTypeId = 1,
                BreedId = 1,
                GiverId = userId,
                ShelterId = shelterId,
                PetVaccines = new List<PetVaccineDto>()
            };

            _shelterRepositoryMock.Setup(s => s.GetByIdAsync(shelterId)).ReturnsAsync(new ShelterDto());
            _userRepositoryMock.Setup(s => s.GetByIdAsync(userId)).ReturnsAsync(new UserDto());

            // Act
            await _service.GivePetAsync(userId, shelterId, petDto);

            // Assert
            _petRepositoryMock.Verify(x => x.CreateAsync(It.Is<PetDto>(p => p.Name == petDto.Name && p.GiverId == userId && p.ShelterId == shelterId)), Times.Once);
        }

        [Test]
        public void WhenGivePetAsync_WithInvalidShelterOrUser_ThenThrowArgumentException()
        {
            // Arrange
            int userId = 1;
            int shelterId = 1;
            var petDto = new PetDto();

            _shelterRepositoryMock.Setup(s => s.GetByIdAsync(shelterId)).ReturnsAsync((ShelterDto)null);
            _userRepositoryMock.Setup(s => s.GetByIdAsync(userId)).ReturnsAsync((UserDto)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.GivePetAsync(userId, shelterId, petDto));
            _petRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<PetDto>()), Times.Never);
        }

        [Test]
        public async Task WhenAdoptPetAsync_WithValidData_ThenUpdateAsync()
        {
            // Arrange
            int userId = 1;
            int petId = 1;
            var petDto = new PetDto()
            {
                Id = petId,
                AdopterId = null,
                IsAdopted = false
            };

            _petRepositoryMock.Setup(p => p.GetByIdAsync(petId)).ReturnsAsync(petDto);
            _userRepositoryMock.Setup(u => u.GetByIdAsync(userId)).ReturnsAsync(new UserDto());

            // Act
            await _service.AdoptPetAsync(userId, petId);

            // Assert
            _petRepositoryMock.Verify(x => x.UpdateAsync(It.Is<PetDto>(p => p.Id == petId && p.AdopterId == userId && p.IsAdopted)), Times.Once);
        }

        [Test]
        public void WhenAdoptPetAsync_WithInvalidPetOrUser_ThenThrowArgumentException()
        {
            // Arrange
            int userId = 1;
            int petId = 1;

            _petRepositoryMock.Setup(p => p.GetByIdAsync(petId)).ReturnsAsync((PetDto)null);
            _userRepositoryMock.Setup(u => u.GetByIdAsync(userId)).ReturnsAsync((UserDto)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.AdoptPetAsync(userId, petId));
            _petRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<PetDto>()), Times.Never);
        }

        [Test]
        public async Task WhenGetAllActiveAsync_ThenReturnAllPets()
        {
            // Arrange
            var petDtos = new List<PetDto>
            {
                new PetDto { Id = 1, Name = "Pet1" },
                new PetDto { Id = 2, Name = "Pet2" },
            };

            _petRepositoryMock.Setup(p => p.GetAllAsync()).ReturnsAsync(petDtos);

            // Act
            var result = await _service.GetAllActiveAsync();

            // Assert
            _petRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(petDtos));
        }
    }
}

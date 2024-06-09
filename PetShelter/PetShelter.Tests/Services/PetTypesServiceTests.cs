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
    public class PetTypesServiceTests
    {
        private readonly Mock<IPetTypeRepository> _petTypeRepositoryMock = new Mock<IPetTypeRepository>();

        private readonly IPetTypesService _service;

        public PetTypesServiceTests()
        {
            _service = new PetTypesService(_petTypeRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            var petTypeDto = new PetTypeDto
            {
                Name = "Dog",
                Pets = new List<PetDto>()
            };

            // Act
            await _service.SaveAsync(petTypeDto);

            //Assert
            _petTypeRepositoryMock.Verify(x => x.SaveAsync(petTypeDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _petTypeRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _petTypeRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidPetTypeId_ThenReturnUser(int petTypeId)
        {
            // Arrange
            var petTypeDto = new PetTypeDto
            {
                Name = "Dog",
                Pets = new List<PetDto>()
            };

            _petTypeRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(petTypeId))))
                .ReturnsAsync(petTypeDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(petTypeId);

            //Assert
            _petTypeRepositoryMock.Verify(x => x.GetByIdAsync(petTypeId), Times.Once);
            Assert.That(userResult == petTypeDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidPetTypeId_ThenReturnDefault(int petTypeId)
        {
            //Arrange
            var petType = (PetTypeDto)default;

            _petTypeRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(petTypeId))))
                .ReturnsAsync(petType);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(petTypeId);

            //Assert
            _petTypeRepositoryMock.Verify(x => x.GetByIdAsync(petTypeId), Times.Once);
            Assert.That(userResult == petType);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var petTypeDto = new PetTypeDto
            {
                Name = "Dog",
                Pets = new List<PetDto>()
            };

            _petTypeRepositoryMock.Setup(s => s.SaveAsync(It.Is<PetTypeDto>(x => x.Equals(petTypeDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(petTypeDto);

            //Assert
            _petTypeRepositoryMock.Verify(x => x.SaveAsync(petTypeDto), Times.Once);
        }
    }
}

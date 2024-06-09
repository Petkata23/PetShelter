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
    public class UsersServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();

        private readonly IUsersService _service;

        public UsersServiceTests()
        {
            _service = new UsersService(_userRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            var userDto = new UserDto
            {
                Username = "username",
                Password = "password",
                FirstName = "Ivan",
                LastName = "Georgiev",
                RoleId = 1, 
                ShelterId = 1, 
                AdoptedPets = new List<PetDto>(),
                GivenPets = new List<PetDto>()
            };

            // Act
            await _service.SaveAsync(userDto);

            //Assert
            _userRepositoryMock.Verify(x => x.SaveAsync(userDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _userRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _userRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidUserId_ThenReturnUser(int userId)
        {
            // Arrange
            var userDto = new UserDto
            {
                Username = "username",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                RoleId = 1,
                ShelterId = 1,
                AdoptedPets = new List<PetDto>(),
                GivenPets = new List<PetDto>()
            };

            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(userId))))
                .ReturnsAsync(userDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(userId);

            //Assert
            _userRepositoryMock.Verify(x => x.GetByIdAsync(userId), Times.Once);
            Assert.That(userResult == userDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidUserId_ThenReturnDefault(int userId)
        {
            //Arrange
            var user = (UserDto)default;

            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(userId))))
                .ReturnsAsync(user);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(userId);

            //Assert
            _userRepositoryMock.Verify(x => x.GetByIdAsync(userId), Times.Once);
            Assert.That(userResult == user);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var userDto = new UserDto
            {
                Username = "username",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                RoleId = 1, // Assuming the role ID
                ShelterId = 1, // Assuming the shelter ID
                AdoptedPets = new List<PetDto>(),
                GivenPets = new List<PetDto>()
            };

            _userRepositoryMock.Setup(s => s.SaveAsync(It.Is<UserDto>(x => x.Equals(userDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(userDto);

            //Assert
            _userRepositoryMock.Verify(x => x.SaveAsync(userDto), Times.Once);
        }
    }
}

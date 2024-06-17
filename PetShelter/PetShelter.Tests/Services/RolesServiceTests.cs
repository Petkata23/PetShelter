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
    public class RolesServiceTests
    {
        private readonly Mock<IRoleRepository> _roleRepositoryMock = new Mock<IRoleRepository>();

        private readonly IRolesService _service;

        public RolesServiceTests()
        {
            _service = new RolesService(_roleRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            var roleDto = new RoleDto
            {
                Name = "Name",
                Users = new List<UserDto>()
            };

            // Act
            await _service.SaveAsync(roleDto);

            //Assert
            _roleRepositoryMock.Verify(x => x.SaveAsync(roleDto), Times.Once);
        }


        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _roleRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never);
        }

        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _roleRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidRoleId_ThenReturnUser(int roleId)
        {
            // Arrange
            var roleDto = new RoleDto
            {
                Name = "Name",
                Users = new List<UserDto>()
            };

            _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(roleId))))
                .ReturnsAsync(roleDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(roleId);

            //Assert
            _roleRepositoryMock.Verify(x => x.GetByIdAsync(roleId), Times.Once);
            Assert.That(userResult == roleDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidRoleId_ThenReturnDefault(int roleId)
        {
            //Arrange
            var role = (RoleDto)default;

            _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(roleId))))
                .ReturnsAsync(role);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(roleId);

            //Assert
            _roleRepositoryMock.Verify(x => x.GetByIdAsync(roleId), Times.Once);
            Assert.That(userResult == role);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var roleDto = new RoleDto
            {
                Name = "Name",
                Users = new List<UserDto>()
            };

            _roleRepositoryMock.Setup(s => s.SaveAsync(It.Is<RoleDto>(x => x.Equals(roleDto))))
                .Verifiable();

            //Act
            await _service.SaveAsync(roleDto);

            //Assert
            _roleRepositoryMock.Verify(x => x.SaveAsync(roleDto), Times.Once);
        }

        [Test]
        public async Task WhenGetAllAsync_ThenReturnAllRoles()
        {
            // Arrange
            var roles = new List<RoleDto>
            {
                new RoleDto { Name = "Admin", Users = new List<UserDto>() },
                new RoleDto { Name = "User", Users = new List<UserDto>() }
            };

            _roleRepositoryMock.Setup(p => p.GetAllAsync()).ReturnsAsync(roles);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _roleRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(roles));
        }

        [TestCase("Admin")]
        [TestCase("User")]
        public async Task WhenGetByNameIfExistsAsync_WithValidRoleName_ThenReturnRole(string roleName)
        {
            // Arrange
            var roleDto = new RoleDto
            {
                Name = roleName,
                Users = new List<UserDto>()
            };

            _roleRepositoryMock.Setup(s => s.GetByNameIfExistsAsync(It.Is<string>(x => x.Equals(roleName))))
                .ReturnsAsync(roleDto);

            // Act
            var roleResult = await _service.GetByNameIfExistsAsync(roleName);

            // Assert
            _roleRepositoryMock.Verify(x => x.GetByNameIfExistsAsync(roleName), Times.Once);
            Assert.That(roleResult, Is.EqualTo(roleDto));
        }

        [TestCase("")]
        [TestCase(" ")]
        public async Task WhenGetByNameIfExistsAsync_WithInvalidRoleName_ThenReturnDefault(string roleName)
        {
            // Arrange
            var role = (RoleDto)default;

            _roleRepositoryMock.Setup(s => s.GetByNameIfExistsAsync(It.Is<string>(x => x.Equals(roleName))))
                .ReturnsAsync(role);

            // Act
            var roleResult = await _service.GetByNameIfExistsAsync(roleName);

            // Assert
            _roleRepositoryMock.Verify(x => x.GetByNameIfExistsAsync(roleName), Times.Once);
            Assert.That(roleResult, Is.EqualTo(role));
        }

    }
}

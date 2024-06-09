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
    }
}

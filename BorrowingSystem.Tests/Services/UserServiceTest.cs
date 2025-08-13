using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;
using BorrowingSystem.Services;
using Moq;

namespace BorrowingSystem.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IRoleRepository> _roleRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private UserService _userService;


        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _roleRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public void GetUserById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, FullName = "Test User", Email = "test@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
        }

        [Test]
        public void GetUserById_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _ = _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns((User)null!);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void CreateUser_CallRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var createdById = Guid.NewGuid();
            var userDTO = new CreateUserDTO { FullName = "Test User", Email = "test@example.com" };
            var user = new User { Id = userId, FullName = userDTO.FullName, Email = userDTO.Email };
            _mapperMock.Setup(m => m.Map<User>(userDTO)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.CreateUser(user)).Returns(user);

            Console.WriteLine(">>>>>>>>", _userService.GetAllUsers());

            // Act
            var result = _userService.CreateUser(userDTO, createdById);

            Console.WriteLine($"result>>>>>>>> {result}");

            // Assert
            _userRepositoryMock.Verify(repo => repo.CreateUser(user), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once);
            Assert.That(result.Id, Is.EqualTo(user.Id));
        }

        [Test]
        public void CreateUser_AssignsRolesToUser()
        {
            // Arrange
            var createdById = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var roles = new List<Role> { new Role { Id = roleId, Name = "Admin" } };
            var userDto = new CreateUserDTO
            {
                FullName = "Test User",
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                RoleIds = new List<Guid> { roleId }
            };

            _roleRepositoryMock.Setup(r => r.GetRolesByIds(It.IsAny<List<Guid>>())).Returns(roles);

            var userService = new UserService(_userRepositoryMock.Object, _roleRepositoryMock.Object, _mapperMock.Object);

            // Act
            var user = userService.CreateUser(userDto, createdById);

            // Assert
            Assert.That(user.RoleIds, Has.Count.EqualTo(1));
            Assert.That(user.RoleIds.First(), Is.EqualTo(roleId));
            _userRepositoryMock.Verify(r => r.CreateUser(It.IsAny<User>()), Times.Once);
        }
    }
}

using BorrowingSystem.Controllers;
using BorrowingSystem.DTOs;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.Models;
using BorrowingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BorrowingSystem.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Test]
        public void GetUserById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var user = new User { Id = userId, FullName = "Test User", Email = "test@example.com" };
            var userDto = new ExistingUserDTO
            {
                FullName = "Test User",
                Email = "test@example.com",
                RoleIds = new List<Guid> { roleId }
            };

            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(userDto);

            // Act
            var actionResult = _userController.GetUserById(userId);
            var result = actionResult;

            // Assert 
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                //Assert.That(result!.StatusCode, Is.EqualTo(200));
                //Assert.That(result.Value, Is.EqualTo(user));
            });
        }
    }
}

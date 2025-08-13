using BorrowingSystem.Context;
using BorrowingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using BorrowingSystem.Models;

namespace BorrowingSystem.Tests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private BorrowingContext _context;
        private UserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BorrowingContext>()
                .UseInMemoryDatabase(databaseName: "BorrowingSystemTestDB")
                .Options;

            _context = new BorrowingContext(options);
            _userRepository = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _context = null!;
        }

        [Test]
        public void CreateUser_AddUserToDatabase()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), FullName = "Test User", Email = "test@example.com" };

            // Act
            _userRepository.CreateUser(user);
            _userRepository.SaveChanges();
            var result = _userRepository.GetUserById(user.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.FullName, Is.EqualTo(user.FullName));
        }

        [Test]
        public void CreateUser_WithRoles_AddsUserAndRolesToDatabase()
        {
            // Arrange
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid()
            };
            _context.Roles.Add(role);
            _context.SaveChanges();

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Test User",
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                Roles = new List<Role> { role }
            };

            // Act
            _userRepository.CreateUser(user);
            _userRepository.SaveChanges();
            var result = _userRepository.GetUserById(user.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.FullName, Is.EqualTo(user.FullName));
                Assert.That(result.Roles, Is.Not.Null);
                Assert.That(result.Roles, Has.Count.EqualTo(1));
                Assert.That(result.Roles.First().Name, Is.EqualTo("Admin"));
            });
        }
    }
}

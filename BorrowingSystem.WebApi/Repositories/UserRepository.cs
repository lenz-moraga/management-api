using BorrowingSystem.Context;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Repositories
{
    public class UserRepository(BorrowingContext context) : IUserRepository
    {
        private readonly BorrowingContext _context = context;

        public async Task<User> CreateUserAsync(User user)
        {
            var newUser = await _context.Users.AddAsync(user);

            return newUser.Entity;
        }

        public void DeleteUser(User user) =>
            user.DeletedAt = DateTime.Now;

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(Guid id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetUserByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public User UpdateUser(User user)
        {
            var trackedUser = _context.ChangeTracker.Entries<User>()
                .FirstOrDefault(u => u.Entity.Id == user.Id)?.Entity;

            if (trackedUser is not null)
            {
                _context.Entry(trackedUser).CurrentValues.SetValues(user);
                return trackedUser;
            }

            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }

        public async Task<bool> UserExistsAsync(string Email) =>
            await _context.Users.AnyAsync(u => u.Email == Email);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}

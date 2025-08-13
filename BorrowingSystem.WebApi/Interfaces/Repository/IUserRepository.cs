using BorrowingSystem.Models;

namespace BorrowingSystem.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        User UpdateUser(User user);
        void DeleteUser(User user);
        Task SaveChangesAsync();
        Task<bool> UserExistsAsync(string Email);
    }
}

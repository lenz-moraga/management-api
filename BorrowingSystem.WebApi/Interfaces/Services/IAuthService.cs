using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthDTO?> LoginAsync(string email, string password);
        Task<bool> UserExistsAsync(string email);
        Guid GetAuthUserId();
    }
}

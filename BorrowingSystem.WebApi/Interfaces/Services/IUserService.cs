using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ExistingUserDTO>> GetAllUsersAsync();
        Task<ExistingUserDTO> GetUserByIdAsync(Guid id);
        Task<ExistingUserDTO> CreateUserAsync(CreateUserDTO userDto);
        Task<ExistingUserDTO> UpdateUserAsync(Guid id, UpdateUserDTO userDto);
        Task DeleteUserAsync(Guid id);
    }
}

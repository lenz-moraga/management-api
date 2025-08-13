using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<ExistingRoleDTO>> GetAllRolesAsync();
        Task<ExistingRoleDTO> GetRoleByIdAsync(Guid id);
        Task<ExistingRoleDTO> CreateRoleAsync(CreateRoleDTO roleDto);
        Task<ExistingRoleDTO> UpdateRoleAsync(Guid id, UpdateRoleDTO roleDto);
        Task DeleteRoleAsync(Guid id);
    }
}

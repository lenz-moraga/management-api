using BorrowingSystem.Models;

namespace BorrowingSystem.Interfaces.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(Guid id);
        Task<IEnumerable<Role>> GetRolesByIdsAsync(List<Guid> ids);
        Task<Role> CreateRoleAsync(Role role);
        Role UpdateRole(Role role);
        void DeleteRole(Role role);
        Task SaveChangesAsync();
        Task<Role?> GetRoleByNameAsync(string name);
        Task<Role?> ExistsByNameAndDifferentId(string name, Guid id);
    }
}

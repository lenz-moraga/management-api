using BorrowingSystem.Context;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Repositories
{
    public class RoleRepository(BorrowingContext context) : IRoleRepository
    {
        private readonly BorrowingContext _context = context;

        public async Task<IEnumerable<Role>> GetAllRolesAsync() =>
            await _context.Roles.ToListAsync();

        public async Task<Role?> GetRoleByIdAsync(Guid id) =>
            await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IEnumerable<Role>> GetRolesByIdsAsync(List<Guid> ids) =>
            await _context.Roles.Where(r => ids.Contains(r.Id)).ToListAsync();

        public async Task<Role> CreateRoleAsync(Role role)
        {
            var newRole = await _context.Roles.AddAsync(role);

            return newRole.Entity;
        }

        public Role UpdateRole(Role role)
        {
            var trackedRole = _context.ChangeTracker.Entries<Role>()
                .FirstOrDefault(r => r.Entity.Id == role.Id)?.Entity;

            if (trackedRole is not null)
            {
                _context.Entry(trackedRole).CurrentValues.SetValues(role);
                return trackedRole;
            }

            _context.Roles.Attach(role);
            _context.Entry(role).State = EntityState.Modified;
            return role;
        }

        public void DeleteRole(Role role) =>
            role.DeletedAt = DateTime.Now;

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<Role?> GetRoleByNameAsync(string name) =>
            await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);

        public async Task<Role?> ExistsByNameAndDifferentId(string name, Guid id) =>
            await _context.Roles.FirstOrDefaultAsync(r => r.Name == name && r.Id != id);
    }
}
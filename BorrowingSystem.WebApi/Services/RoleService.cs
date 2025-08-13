using AutoMapper;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services
{
    public class RoleService(IRoleRepository roleRepository, IMapper mapper) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ExistingRoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();

            return _mapper.Map<IEnumerable<ExistingRoleDTO>>(roles);
        }

        public async Task<ExistingRoleDTO> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id)
                ?? throw new ServiceException("Role not found.", ErrorCode.NotFound);

            return _mapper.Map<ExistingRoleDTO>(role);
        }

        public async Task<ExistingRoleDTO> CreateRoleAsync(CreateRoleDTO roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            var newRole = await _roleRepository.CreateRoleAsync(role);

            await _roleRepository.SaveChangesAsync();

            return _mapper.Map<ExistingRoleDTO>(newRole);
        }

        public async Task<ExistingRoleDTO> UpdateRoleAsync(Guid id, UpdateRoleDTO roleDto)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(id)
                ?? throw new ServiceException("Role not found.", ErrorCode.NotFound);

            _mapper.Map(roleDto, existingRole);

            var updatedRole = _roleRepository.UpdateRole(existingRole);
            await _roleRepository.SaveChangesAsync();

            return _mapper.Map<ExistingRoleDTO>(updatedRole);
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id)
                ?? throw new ServiceException("Role not found.", ErrorCode.NotFound);

            _roleRepository.DeleteRole(role);
            await _roleRepository.SaveChangesAsync();
        }
    }
}
using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services
{
    public class UserService(IUserRepository userRepository, IRoleRepository roleRepository, IAuthService authService, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ExistingUserDTO>> GetAllUsersAsync()
        {
            var users = _mapper.Map<IEnumerable<ExistingUserDTO>>(await _userRepository.GetAllUsersAsync());

            return users;
        }

        public async Task<ExistingUserDTO> GetUserByIdAsync(Guid id)
        {
            var user = _mapper.Map<ExistingUserDTO>(await _userRepository.GetUserByIdAsync(id))
                ?? throw new ServiceException("User not found!", ErrorCode.NotFound);

            return user;
        }

        public async Task<ExistingUserDTO?> GetUserByEmailAsync(string email)
        {
            var user = _mapper.Map<ExistingUserDTO>(await _userRepository.GetUserByEmailAsync(email))
                ?? throw new ServiceException("User not found!", ErrorCode.NotFound);

            return user;
        }

        public async Task<ExistingUserDTO> CreateUserAsync(CreateUserDTO userDto)
        {
            var roles = await _roleRepository.GetRolesByIdsAsync(userDto.RoleIds)
                ?? throw new ServiceException("At least one valid role is required.", ErrorCode.NotFound);

            var createdBy = _authService.GetAuthUserId();

            var user = _mapper.Map<User>(userDto);
            user.Roles = [.. roles];
            user.CreatedAt = DateTime.UtcNow;
            user.CreatedBy = createdBy;

            var newUser = _mapper.Map<ExistingUserDTO>(await _userRepository.CreateUserAsync(user));
            await _userRepository.SaveChangesAsync();

            return newUser;
        }

        public async Task<ExistingUserDTO> UpdateUserAsync(Guid id, UpdateUserDTO userDto)
        {

            var existingUser = await _userRepository.GetUserByIdAsync(id)
                ?? throw new ServiceException("User not found.", ErrorCode.NotFound);

            _mapper.Map(userDto, existingUser);

            var updatedUser = _mapper.Map<ExistingUserDTO>(_userRepository.UpdateUser(existingUser));
            await _userRepository.SaveChangesAsync();

            return updatedUser;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id)
                ?? throw new ServiceException("User not found.", ErrorCode.NotFound);

            _userRepository.DeleteUser(existingUser);
            await _userRepository.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Interfaces.Services;
using System.Security.Claims;

namespace BorrowingSystem.Services
{
    public class AuthService(ITokenService tokenService, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;

        public async Task<AuthDTO?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email)
                ?? throw new ServiceException($"User with email: {email} was not found!", ErrorCode.NotFound);

            if (!VerifyPasswordHash(password, user.PasswordHash))
                throw new ServiceException("Invalid credentials.", ErrorCode.NotFound);

            var authUserDto = _mapper.Map<AuthDTO>(user);

            var token = _tokenService.CreateToken(authUserDto);
            authUserDto.Token = token;

            return authUserDto;
        }

        private static bool VerifyPasswordHash(string password1, string password2) => 
            password1 == password2;

        public async Task<bool> UserExistsAsync(string email) =>
            await _userRepository.UserExistsAsync(email);

        public Guid GetAuthUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new ServiceException("Action not Authorized", ErrorCode.Unauthorized);

            return Guid.Parse(userId);
        }
    }
}

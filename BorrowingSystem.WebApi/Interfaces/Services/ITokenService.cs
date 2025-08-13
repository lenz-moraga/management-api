using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(AuthDTO user);
    }
}

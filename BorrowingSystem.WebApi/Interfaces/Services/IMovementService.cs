using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface IMovementService
    {
        Task<IEnumerable<ExistingMovementDTO>> GetAllMovementsAsync();
        Task<ExistingMovementDTO?> GetMovementByIdAsync(Guid id);
        Task<ExistingMovementDTO> CreateMovementAsync(CreateMovementDTO movementDto);
        Task<ExistingMovementDTO?> UpdateMovementAsync(Guid id, UpdateMovementDTO movementDto);
        Task DeleteMovementAsync(Guid id);
    }
}

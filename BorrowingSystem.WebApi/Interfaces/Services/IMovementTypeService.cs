using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface IMovementTypeService
    {
        Task<IEnumerable<ExistingMovementTypeDTO>> GetAllMovementTypesAsync();
        Task<ExistingMovementTypeDTO?> GetMovementTypeByIdAsync(Guid id);
        Task<ExistingMovementTypeDTO> CreateMovementTypeAsync(CreateMovementTypeDTO movementDto);
        Task<ExistingMovementTypeDTO?> UpdateMovementTypeAsync(Guid id, UpdateMovementTypeDTO movementDto);
        Task DeleteMovementTypeAsync(Guid id);
    }
}

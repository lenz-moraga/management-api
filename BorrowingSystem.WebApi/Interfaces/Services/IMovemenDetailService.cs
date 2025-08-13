using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface IMovementDetailService
    {
        Task<IEnumerable<ExistingMovementDetailsDTO>> GetAllMovementDetailItemsAsync();
        Task<ExistingMovementDetailsDTO?> GetMovementDetailItemByIdAsync(Guid id);
        Task<ExistingMovementDetailsDTO> CreateMovementDetailItemAsync(CreateMovementDetailsDTO movementDetailItemDto);
        Task<ExistingMovementDetailsDTO?> UpdateRequestItemAsync(Guid id, UpdateMovementDetailsDTO requestItem);
        Task DeleteRequestItemAsync(Guid id);
    }
}

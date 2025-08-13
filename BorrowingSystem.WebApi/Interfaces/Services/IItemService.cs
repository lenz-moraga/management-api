using BorrowingSystem.DTOs;

namespace BorrowingSystem.Interfaces.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ExistingItemDTO>> GetAllItemsAsync();
        Task<ExistingItemDTO?> GetItemByIdAsync(Guid id);
        Task<ExistingItemDTO> CreateItemAsync(CreateItemDTO itemDto);
        Task<ExistingItemDTO?> UpdateItemAsync(Guid id, UpdateItemDTO itemDto);
        Task DeleteItemAsync(Guid id);
    }
}

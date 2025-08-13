using BorrowingSystem.Models;

namespace BorrowingSystem.Interfaces.Repository
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(Guid id);
        Task<Item> CreateItemAsync(Item item);
        Item UpdateItem(Item item);
        void DeleteItem(Item item);
        Task<bool> ItemExistsAsync(Guid id);
        Task<int?> GetItemQuantityAsync(Guid id);
        Task SaveChangesAsync();
    }
}

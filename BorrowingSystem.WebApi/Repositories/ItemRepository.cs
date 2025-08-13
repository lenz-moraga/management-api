using BorrowingSystem.Context;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Repositories
{
    public class ItemRepository(BorrowingContext context) : IItemRepository
    {
        private readonly BorrowingContext _context = context;

        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            await _context.Items.ToListAsync();
        public async Task<Item?> GetItemByIdAsync(Guid id) =>
            await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

        public async Task<Item> CreateItemAsync(Item item)
        {
            var newItem = await _context.Items.AddAsync(item);

            return newItem.Entity;
        }

        public Item UpdateItem(Item item)
        {
            var trackedEntity = _context.ChangeTracker.Entries<Item>()
                .FirstOrDefault(e => e.Entity.Id == item.Id)?.Entity;

            if (trackedEntity is not null)
            {
                _context.Entry(trackedEntity).CurrentValues.SetValues(item);
                return trackedEntity;
            }

            _context.Items.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }

        public void DeleteItem(Item item) =>
            item.DeletedAt = DateTime.Now;

        public async Task<bool> ItemExistsAsync(Guid id) =>
            await _context.Items.AnyAsync(i => i.Id == id);

        public async Task<int?> GetItemQuantityAsync(Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

            return item?.CurrentStock;
        }

        public async Task SaveChangesAsync() => 
            await _context.SaveChangesAsync();
    }
}

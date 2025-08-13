using BorrowingSystem.Context;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Repositories
{
    public class MovementDetailsRepository(BorrowingContext context) : IMovementDetailRepository
    {
        private readonly BorrowingContext _context = context;

        public async Task<MovementDetail> CreateMovementDetailItemAsync(MovementDetail movementItem)
        {
            var newMovementDetailItem = await _context.MovementDetails.AddAsync(movementItem);

            return newMovementDetailItem.Entity;
        }

        public void DeleteMovementDetailItem(MovementDetail movementDetailItemToDelete) =>
            movementDetailItemToDelete.DeletedAt = DateTime.Now; // no need for the update here, as the context tracks changes

        public async Task<IEnumerable<MovementDetail>> GetAllMovementDetailItemsAsync() =>
            await _context.MovementDetails.ToListAsync();

        public async Task<MovementDetail?> GetMovementDetailItemByIdAsync(Guid id) =>
            await _context.MovementDetails.FirstOrDefaultAsync(u => u.Id == id);

        public MovementDetail UpdateMovementDetailItem(MovementDetail movementDetailItem)
        {
            var trackedEntity = _context.ChangeTracker.Entries<MovementDetail>()
                .FirstOrDefault(e => e.Entity.Id == movementDetailItem.Id)?.Entity;
            if (trackedEntity is not null)
            {
                _context.Entry(trackedEntity).CurrentValues.SetValues(movementDetailItem);
                return trackedEntity;
            }

            _context.MovementDetails.Attach(movementDetailItem);
            _context.Entry(movementDetailItem).State = EntityState.Modified;
            return movementDetailItem;
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}

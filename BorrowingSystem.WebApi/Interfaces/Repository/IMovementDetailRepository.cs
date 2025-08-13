using BorrowingSystem.Models;

namespace BorrowingSystem.Interfaces.Repository
{
    public interface IMovementDetailRepository
    {
        Task<MovementDetail> CreateMovementDetailItemAsync(MovementDetail requestItem);
        Task<IEnumerable<MovementDetail>> GetAllMovementDetailItemsAsync();
        Task<MovementDetail?> GetMovementDetailItemByIdAsync(Guid id);
        Task SaveChangesAsync();
        void DeleteMovementDetailItem(MovementDetail requestItem);
        MovementDetail UpdateMovementDetailItem(MovementDetail requestItem);
    }
}

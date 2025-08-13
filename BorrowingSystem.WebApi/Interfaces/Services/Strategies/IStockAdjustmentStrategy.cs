using BorrowingSystem.Models;

namespace BorrowingSystem.Interfaces.Services.Strategies
{
    public interface IStockAdjustmentStrategy
    {
        void Apply(Item item, MovementDetail movementDetail);
    }
}

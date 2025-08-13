using BorrowingSystem.Interfaces.Services.Strategies;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services.Strategies
{
    public class RestockStrategy : IStockAdjustmentStrategy
    {
        public void Apply(Item item, MovementDetail movementDetail)
        {
            item.CurrentStock += movementDetail.Quantity;
        }
    }

}

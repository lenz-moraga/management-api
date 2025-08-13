using BorrowingSystem.Interfaces.Services.Strategies;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services.Strategies
{
    public class ReturnStrategy : IStockAdjustmentStrategy
    {
        public void Apply(Item item, MovementDetail movementDetail)
        {
            item.CurrentStock += movementDetail.Quantity;
        }
    }

}

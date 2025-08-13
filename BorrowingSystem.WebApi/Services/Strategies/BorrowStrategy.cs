using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Services.Strategies;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services.Strategies
{
    public class BorrowStrategy : IStockAdjustmentStrategy
    {
        public void Apply(Item item, MovementDetail movementDetail)
        {
            if (item.CurrentStock < movementDetail.Quantity)
                throw new ServiceException(
                    $"Insufficient stock for item {item.Name}. Current: {item.CurrentStock}, Requested: {movementDetail.Quantity}",
                    ErrorCode.BadRequest);

            item.CurrentStock -= movementDetail.Quantity;
        }
    }

}

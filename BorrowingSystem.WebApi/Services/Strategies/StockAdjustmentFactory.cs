using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Services.Strategies;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services.Strategies
{
    public class StockAdjustmentFactory
    {
        private readonly IDictionary<string, IStockAdjustmentStrategy> _strategies;

        public StockAdjustmentFactory(IEnumerable<IStockAdjustmentStrategy> strategies)
        {
            _strategies = new Dictionary<string, IStockAdjustmentStrategy>(StringComparer.OrdinalIgnoreCase);

            foreach (var strategy in strategies)
            {
                var key = strategy.GetType().Name.Replace("Strategy", string.Empty); // e.g., "Borrow", "Return"
                _strategies[key] = strategy;
            }
        }

        public void ApplyStrategy(Item item, MovementDetail movementDetail)
        {
            var type = movementDetail.MovementType?.Name;

            if (type is null || !_strategies.TryGetValue(type, out var strategy))
                throw new ServiceException($"Unsupported movement type '{type}'", ErrorCode.BadRequest);

            strategy.Apply(item, movementDetail);
        }
    }

}

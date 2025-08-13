using BorrowingSystem.Models;

namespace BorrowingSystem.Interfaces.Repository
{
    public interface IMovementTypeRepository
    {
        Task<MovementType> CreateMovementTypeAsync(MovementType movementType);
        Task<IEnumerable<MovementType>> GetAllMovementTypesAsync();
        Task<MovementType?> GetMovementTypeByIdAsync(Guid id);
        Task SaveChangesAsync();
        MovementType UpdateMovementType(MovementType movementType);
        void DeleteMovementType(MovementType movementType);
    }
}

using BorrowingSystem.Models;

namespace BorrowingSystem.Interfaces.Repository
{
    public interface IMovementRepository
    {
        Task<IEnumerable<Movement>> GetAllMovementsAsync();
        Task<IEnumerable<Movement>> GetAllMovementsWithDetailsAsync(bool includeSoftDeleted = false);
        Task<Movement?> GetMovementByIdAsync(Guid id);
        //Task<Movement?> GetMovementByIdWithDetailsAsync(Guid id, bool includeSoftDeleted = false);
        Task<Movement> CreateMovementAsync(Movement movement);
        Movement UpdateMovement(Movement movement);
        void DeleteMovement(Movement movement);
        Task SaveChangesAsync();
    }
}

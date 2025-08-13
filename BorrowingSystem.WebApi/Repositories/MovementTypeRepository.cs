using BorrowingSystem.Context;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Repositories
{
    public class MovementTypeRepository(BorrowingContext context) : IMovementTypeRepository
    {
        private readonly BorrowingContext _context = context;

        public async Task<MovementType> CreateMovementTypeAsync(MovementType movementType)
        {
            var newMovementType = await _context.MovementTypes.AddAsync(movementType);

            return newMovementType.Entity;
        }

        public void DeleteMovementType(MovementType movement) =>
            movement.DeletedAt = DateTime.Now;

        public async Task<IEnumerable<MovementType>> GetAllMovementTypesAsync() =>
            await _context.MovementTypes.ToListAsync();

        public async Task<MovementType?> GetMovementTypeByIdAsync(Guid id) =>
            await _context.MovementTypes.FirstOrDefaultAsync(r => r.Id == id);

        public MovementType UpdateMovementType(MovementType movementType)
        {
            var trackedEntity = _context.ChangeTracker.Entries<MovementType>()
                .FirstOrDefault(e => e.Entity.Id == movementType.Id)?.Entity;
            if (trackedEntity is not null)
            {
                _context.Entry(trackedEntity).CurrentValues.SetValues(movementType);
                return trackedEntity;
            }
            else
            {
                _context.MovementTypes.Attach(movementType);
                _context.Entry(movementType).State = EntityState.Modified;
                return movementType;
            }
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}

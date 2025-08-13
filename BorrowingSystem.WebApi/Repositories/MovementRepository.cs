using BorrowingSystem.Context;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Repositories
{
    public class MovementRepository(BorrowingContext context) : IMovementRepository
    {
        private readonly BorrowingContext _context = context;

        public async Task<Movement> CreateMovementAsync(Movement movement)
        {
            var newMovement = await _context.Movements.AddAsync(movement);

            return newMovement.Entity;
        }

        public void DeleteMovement(Movement movement) =>
            movement.DeletedAt = DateTime.Now;

        public async Task<IEnumerable<Movement>> GetAllMovementsAsync() =>
            await _context.Movements.ToListAsync();

        public async Task<Movement?> GetMovementByIdAsync(Guid id) =>
            await _context.Movements.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<Movement>> GetAllMovementsWithDetailsAsync(bool includeSoftDeleted = false)
        {
            var query = _context.Movements
                .Include(m => m.MovementDetails)
                    .ThenInclude(md => md.Item)
                .Include(m => m.MovementDetails)
                    .ThenInclude(md => md.MovementType)
                .AsQueryable();

            if (includeSoftDeleted) query = query.IgnoreQueryFilters();

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Movement>> GetMovementByIdWithDetailsAsync(Guid id, bool includeSoftDeleted = false)
        {
            var query = GetAllMovementsWithDetailsAsync(includeSoftDeleted).Result.AsQueryable();

            return await query.Where(m => m.Id == id).ToListAsync();
        }

        public Movement UpdateMovement(Movement movement)
        {
            var trackedEntity = _context.ChangeTracker.Entries<Movement>()
                .FirstOrDefault(e => e.Entity.Id == movement.Id)?.Entity;

            if (trackedEntity is not null)
            {
                _context.Entry(trackedEntity).CurrentValues.SetValues(movement);
                return trackedEntity;
            }

            _context.Movements.Attach(movement);
            _context.Entry(movement).State = EntityState.Modified;
            return movement;
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}

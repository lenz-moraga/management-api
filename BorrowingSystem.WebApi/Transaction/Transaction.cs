using BorrowingSystem.Context;
using BorrowingSystem.Interfaces.Transaction;
using Microsoft.EntityFrameworkCore.Storage;

namespace BorrowingSystem.Transaction
{
    public class Transaction : ITransaction
    {
        private readonly BorrowingContext _context;
        private IDbContextTransaction? _transaction;

        public Transaction(BorrowingContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction to commit.");
            }

            await _transaction.CommitAsync();
            await DisposeTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction to rollback.");
            }

            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
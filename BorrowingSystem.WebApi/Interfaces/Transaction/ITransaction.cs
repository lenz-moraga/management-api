namespace BorrowingSystem.Interfaces.Transaction
{
    public interface ITransaction
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}

namespace BorrowingSystem.DTOs
{
    public class CreateMovementDetailsDTO
    {
        public Guid ItemId { get; set; }
        public Guid MovementTypeId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }

    public class MovementDetailWithExistingMovementId : CreateMovementDetailsDTO
    {
        public Guid MovementId { get; set; }
    }

    public class UpdateMovementDetailsDTO : CreateMovementDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid MovementId { get; set; } // This is necessary to link the detail to its movement
    }

    public class ExistingMovementDetailsDTO : UpdateMovementDetailsDTO { }
}

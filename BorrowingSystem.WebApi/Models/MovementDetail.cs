namespace BorrowingSystem.Models
{
    public class MovementDetail
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid MovementTypeId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public Guid MovementId { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        public virtual Item Item { get; set; } = null!;
        public virtual MovementType MovementType { get; set; } = null!;
        public virtual Movement Movement { get; set; } = null!;
    }
}

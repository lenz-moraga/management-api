namespace BorrowingSystem.Models
{
    public enum MovementStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Movement
    {
        public Guid Id { get; set; }
        public MovementStatus Status { get; set; }
        public string? Comment { get; set; }
        public Guid RequestedByUserId { get; set; }
        public Guid? ApprovedBy { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        public virtual User? RequestedBy { get; set; }
        public ICollection<MovementDetail> MovementDetails { get; set; } = [];
    }
}

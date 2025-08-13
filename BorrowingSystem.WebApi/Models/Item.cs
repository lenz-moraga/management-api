namespace BorrowingSystem.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Code { get; set; }
        public int CurrentStock { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        public virtual ICollection<MovementDetail> MovementDetails { get; set; } = new List<MovementDetail>();
    }
}

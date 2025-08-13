namespace BorrowingSystem.Models
{
    public class MovementType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Audit fields
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

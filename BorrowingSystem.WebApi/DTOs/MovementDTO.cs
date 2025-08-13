using BorrowingSystem.Models;

namespace BorrowingSystem.DTOs
{
    public class CreateMovementDTO
    {
        public MovementStatus Status { get; set; } = MovementStatus.Pending; // Default status
        public string? Comment { get; set; }
        public Guid RequestedByUserId { get; set; }
        public List<MovementDetailWithExistingMovementId> MovementDetails { get; set; } = [];
    }

    public class UpdateMovementDTO : CreateMovementDTO
    {
        public Guid Id { get; set; }
    }

    public class ExistingMovementDTO : UpdateMovementDTO { }
}

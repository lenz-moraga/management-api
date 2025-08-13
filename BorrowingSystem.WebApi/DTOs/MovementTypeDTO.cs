namespace BorrowingSystem.DTOs
{
    public class CreateMovementTypeDTO
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateMovementTypeDTO : CreateMovementTypeDTO
    {
        public Guid Id { get; set; }
    }

    public class ExistingMovementTypeDTO : UpdateMovementTypeDTO { }
}

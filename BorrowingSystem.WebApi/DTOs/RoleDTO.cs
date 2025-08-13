using System.ComponentModel.DataAnnotations;

namespace BorrowingSystem.DTOs
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateRoleDTO : CreateRoleDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
    }

    public class ExistingRoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

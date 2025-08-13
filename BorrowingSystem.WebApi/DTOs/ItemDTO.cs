using System.ComponentModel.DataAnnotations;

namespace BorrowingSystem.DTOs
{
    public class CreateItemDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [MinLength(5, ErrorMessage = "The Name field must have at least 5 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Description field is required.")]
        [MinLength(5, ErrorMessage = "The Description field must have at least 5 characters.")]
        public string Description { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "The Code field must have at least 5 characters.")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "The CurrentStock field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The CurrentStock field must be a positive integer.")]
        public int CurrentStock { get; set; }
    }

    public class UpdateItemDTO : CreateItemDTO
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public Guid Id { get; set; }
    }

    public class ExistingItemDTO : UpdateItemDTO { }
}
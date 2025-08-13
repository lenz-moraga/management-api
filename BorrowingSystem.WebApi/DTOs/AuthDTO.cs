namespace BorrowingSystem.DTOs
{
    public class AuthDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<ExistingRoleDTO> Roles { get; set; } = new();
        public string Token { get; set; } = string.Empty;
    }
}

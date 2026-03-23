using Mooc.Model.Entity;

namespace Mooc.Application.Contracts.Dto.User
{
    public class UpdateUserDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public string? Bio { get; set; }
        public List<string>? Roles { get; set; }
    }
}

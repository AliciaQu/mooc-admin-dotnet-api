namespace Mooc.Application.Contracts.Dto.User
{
    public class UserRoleMetaDto
    {
        public bool Exists { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}

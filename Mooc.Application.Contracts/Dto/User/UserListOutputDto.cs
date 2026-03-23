namespace Mooc.Application.Contracts.Dto.User
{
    public class UserListOutputDto
    {
        public List<UserOutputDto> Items { get; set; } = new();
        public int Total { get; set; }
    }
}

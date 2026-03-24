using Mooc.Application.Contracts.Dto.User;

namespace Mooc.Application.Contracts.System
{
    public interface IUserService
    {
        Task<UserOutputDto> GetByIdAsync(int id);
        Task<UserOutputDto> GetByUserNameAsync(string userName);
        Task<UserListOutputDto> GetListAsync(int page, int pageSize);
        Task<int> CreateAsync(CreateUserDto input, string hashedPassword);
        Task DeleteAsync(List<int> ids);
        Task UpdateAsync(int id, UpdateUserDto input, string? hashedPassword);
        Task<UserRoleMetaDto> GetRoleMetaAsync(int id);
    }
}

using Mooc.Application.Contracts.Dto.User;

namespace Mooc.Application.Contracts.System
{
    public interface IUserService
    {
        Task<UserOutputDto> GetByIdAsync(long id);
        Task<UserOutputDto> GetByUserNameAsync(string userName);
        Task<UserListOutputDto> GetListAsync(int page, int pageSize);
        Task<long> CreateAsync(CreateUserDto input, string hashedPassword);
        Task DeleteAsync(List<long> ids);
        Task UpdateAsync(long id, UpdateUserDto input, string? hashedPassword);
        Task<UserRoleMetaDto> GetRoleMetaAsync(long id);
    }
}

using Mooc.Application.Contracts.Dto.User;
using Mooc.Application.Contracts.System;

namespace Mooc.Application.System
{
    public class UserService : IUserService
    {
        private readonly MoocDBContext _context;

        public UserService(MoocDBContext context)
        {
            _context = context;
        }

        private static UserOutputDto ToOutputDto(User user) => new()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Address = user.Address,
            Gender = user.Gender,
            Dob = user.Dob,
            Avatar = user.Avatar,
            Bio = user.Bio,
            Roles = user.Roles?.Select(r => r.Name).ToList() ?? new(),
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };

        public async Task<UserOutputDto> GetByIdAsync(long id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new EntityNotFoundException($"User {id} not found.");

            return ToOutputDto(user);
        }

        public async Task<UserOutputDto> GetByUserNameAsync(string userName)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                throw new EntityNotFoundException($"User '{userName}' not found.");

            return ToOutputDto(user);
        }

        public async Task<UserListOutputDto> GetListAsync(int page, int pageSize)
        {
            var query = _context.Users
                .Include(u => u.Roles)
                .OrderBy(u => u.Id);

            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new UserListOutputDto
            {
                Items = items.Select(ToOutputDto).ToList(),
                Total = total
            };
        }

        public async Task<long> CreateAsync(CreateUserDto input, string hashedPassword)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();

            var roles = new List<Role>();
            foreach (var roleName in input.Roles)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                if (role == null)
                    throw new EntityNotFoundException($"Role '{roleName}' not found.");
                roles.Add(role);
            }

            var user = new User
            {
                Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                UserName = input.UserName,
                Password = hashedPassword,
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Phone = input.Phone ?? string.Empty,
                Address = input.Address ?? string.Empty,
                Gender = input.Gender,
                Dob = input.Dob,
                Avatar = input.Avatar ?? string.Empty,
                Bio = input.Bio ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userRoles = roles.Select(r => new UserRole
            {
                Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                UserId = user.Id,
                RoleId = r.Id,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            _context.UserRoles.AddRange(userRoles);
            await _context.SaveChangesAsync();

            await tx.CommitAsync();
            return user.Id;
        }

        public async Task DeleteAsync(List<long> ids)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();

            await _context.UserRoles
                .Where(ur => ids.Contains(ur.UserId))
                .ExecuteDeleteAsync();

            await _context.Users
                .Where(u => ids.Contains(u.Id))
                .ExecuteDeleteAsync();

            await tx.CommitAsync();
        }

        public async Task UpdateAsync(long id, UpdateUserDto input, string? hashedPassword)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                throw new EntityNotFoundException($"User {id} not found.");

            if (input.Roles != null)
            {
                await _context.UserRoles
                    .Where(ur => ur.UserId == id)
                    .ExecuteDeleteAsync();

                var roles = new List<Role>();
                foreach (var roleName in input.Roles)
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                    if (role == null)
                        throw new EntityNotFoundException($"Role '{roleName}' not found.");
                    roles.Add(role);
                }

                _context.UserRoles.AddRange(roles.Select(r => new UserRole
                {
                    Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    UserId = id,
                    RoleId = r.Id,
                    CreatedAt = DateTime.UtcNow
                }));
                await _context.SaveChangesAsync();
            }

            bool hasProfileUpdates = false;

            if (input.UserName != null)   { user.UserName  = input.UserName;  hasProfileUpdates = true; }
            if (input.Email != null)      { user.Email     = input.Email;     hasProfileUpdates = true; }
            if (input.FirstName != null)  { user.FirstName = input.FirstName; hasProfileUpdates = true; }
            if (input.LastName != null)   { user.LastName  = input.LastName;  hasProfileUpdates = true; }
            if (input.Phone != null)      { user.Phone     = input.Phone;     hasProfileUpdates = true; }
            if (input.Address != null)    { user.Address   = input.Address;   hasProfileUpdates = true; }
            if (input.Gender != null)     { user.Gender    = input.Gender;    hasProfileUpdates = true; }
            if (input.Dob != null)        { user.Dob       = input.Dob;       hasProfileUpdates = true; }
            if (input.Avatar != null)     { user.Avatar    = input.Avatar;    hasProfileUpdates = true; }
            if (input.Bio != null)        { user.Bio       = input.Bio;       hasProfileUpdates = true; }
            if (hashedPassword != null)   { user.Password  = hashedPassword;  hasProfileUpdates = true; }

            if (hasProfileUpdates)
            {
                user.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            await tx.CommitAsync();
        }

        public async Task<UserRoleMetaDto> GetRoleMetaAsync(long id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return new UserRoleMetaDto { Exists = false };

            return new UserRoleMetaDto
            {
                Exists = true,
                Roles = user.Roles?.Select(r => r.Name).ToList() ?? new()
            };
        }
    }
}

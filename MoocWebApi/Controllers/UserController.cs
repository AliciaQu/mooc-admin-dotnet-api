using Mooc.Application.Contracts.Dto.User;
using Mooc.Application.Contracts.System;
using Mooc.Core.ExceptionHandling;
using System.Security.Claims;

namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    private record CallerRoleContext(
        long CallerId,
        List<string> Roles,
        bool IsSuperAdmin,
        bool IsAdmin,
        List<string> AllowedManagedRoles);

    private CallerRoleContext GetRoleContext()
    {
        var callerId = long.Parse(User.FindFirst("id")?.Value ?? "0");
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value.Trim().ToLower()).ToList();
        var isSuperAdmin = roles.Contains("super admin");
        var isAdmin = isSuperAdmin || roles.Contains("admin");
        var allowedManagedRoles = isSuperAdmin
            ? new List<string> { "admin", "teacher", "student" }
            : isAdmin
                ? new List<string> { "teacher", "student" }
                : new List<string>();

        return new CallerRoleContext(callerId, roles, isSuperAdmin, isAdmin, allowedManagedRoles);
    }

    // POST /api/users
    [HttpPost]
    public async Task<object> CreateUser([FromBody] CreateUserDto dto)
    {
        var ctx = GetRoleContext();
        if (!ctx.IsAdmin)
            throw new MoocAuthorizationException("Forbidden.");

        var requestedRoles = dto.Roles
            .Select(r => r.Trim().ToLower())
            .Distinct()
            .ToList();

        if (requestedRoles.Count == 0)
            requestedRoles = new List<string> { "student" };

        var assignable = ctx.IsSuperAdmin
            ? ctx.AllowedManagedRoles.Append("super admin").ToHashSet()
            : ctx.AllowedManagedRoles.ToHashSet();

        if (requestedRoles.Any(r => !assignable.Contains(r)))
            throw new MoocAuthorizationException("Cannot assign one or more of the requested roles.");

        dto.Roles = requestedRoles;
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, 10);
        var newId = await _userService.CreateAsync(dto, hashedPassword);
        return new { id = newId };
    }

    // GET /api/users
    [HttpGet]
    public async Task<UserListOutputDto> ListUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var ctx = GetRoleContext();
        if (!ctx.IsAdmin)
            throw new MoocAuthorizationException("Forbidden.");

        var result = await _userService.GetListAsync(page, pageSize);

        var filtered = result.Items
            .Where(u =>
                u.Roles.All(r => ctx.AllowedManagedRoles.Contains(r.ToLower()))
                || u.Id == ctx.CallerId)
            .ToList();

        return new UserListOutputDto { Items = filtered, Total = filtered.Count };
    }

    // GET /api/users/{idOrName}
    [HttpGet("{idOrName}")]
    public async Task<UserOutputDto> GetProfile(string idOrName)
    {
        var ctx = GetRoleContext();

        if (long.TryParse(idOrName, out var targetId))
        {
            if (!ctx.IsAdmin && targetId != ctx.CallerId)
                throw new MoocAuthorizationException("Forbidden.");

            if (ctx.IsAdmin && targetId != ctx.CallerId)
            {
                var meta = await _userService.GetRoleMetaAsync(targetId);
                if (!meta.Exists)
                    throw new EntityNotFoundException("User not found");

                var targetRoles = meta.Roles.Select(r => r.ToLower()).ToList();
                if (targetRoles.Contains("super admin") || targetRoles.Any(r => !ctx.AllowedManagedRoles.Contains(r)))
                    throw new MoocAuthorizationException("Forbidden.");
            }

            return await _userService.GetByIdAsync(targetId);
        }
        else
        {
            return await _userService.GetByUserNameAsync(idOrName);
        }
    }

    // PUT /api/users/{id}
    [HttpPut("{id}")]
    public async Task<object> UpdateProfile(long id, [FromBody] UpdateUserDto input)
    {
        var ctx = GetRoleContext();

        if (!ctx.IsAdmin && id != ctx.CallerId)
            throw new MoocAuthorizationException("Forbidden.");

        if (ctx.IsAdmin && id != ctx.CallerId)
        {
            var meta = await _userService.GetRoleMetaAsync(id);
            if (!meta.Exists)
                throw new EntityNotFoundException("User not found");

            var targetRoles = meta.Roles.Select(r => r.ToLower()).ToList();
            if (targetRoles.Contains("super admin") || targetRoles.Any(r => !ctx.AllowedManagedRoles.Contains(r)))
                throw new MoocAuthorizationException("Forbidden.");
        }

        if (input.Roles != null && !ctx.IsAdmin)
            throw new MoocAuthorizationException("Forbidden.");

        if (input.Roles != null && ctx.IsAdmin)
        {
            var normalizedRoles = input.Roles
                .Select(r => r.Trim().ToLower())
                .Distinct()
                .ToList();

            var assignable = ctx.IsSuperAdmin
                ? ctx.AllowedManagedRoles.Append("super admin").ToHashSet()
                : ctx.AllowedManagedRoles.ToHashSet();

            if (normalizedRoles.Any(r => !assignable.Contains(r)))
                throw new MoocAuthorizationException("Cannot assign one or more of the requested roles.");

            input.Roles = normalizedRoles;
        }

        string? hashedPassword = null;
        if (!string.IsNullOrEmpty(input.Password))
            hashedPassword = BCrypt.Net.BCrypt.HashPassword(input.Password, 10);

        await _userService.UpdateAsync(id, input, hashedPassword);
        return new { };
    }

    // DELETE /api/users/{ids}
    [HttpDelete("{ids}")]
    public async Task<object> DeleteUsers(string ids)
    {
        var ctx = GetRoleContext();
        if (!ctx.IsAdmin)
            throw new MoocAuthorizationException("Forbidden.");

        List<long> idList;
        try
        {
            idList = ids.Split(',').Select(s => long.Parse(s.Trim())).ToList();
        }
        catch
        {
            throw new UserFriendlyException("Invalid ids format. Expected comma-separated integers.");
        }

        foreach (var id in idList)
        {
            var meta = await _userService.GetRoleMetaAsync(id);
            if (!meta.Exists)
                throw new EntityNotFoundException("User not found");

            var targetRoles = meta.Roles.Select(r => r.ToLower()).ToList();
            if (targetRoles.Contains("super admin") && id != ctx.CallerId)
                throw new MoocAuthorizationException($"Forbidden: cannot delete user {id}.");
            if (targetRoles.Any(r => !ctx.AllowedManagedRoles.Contains(r)))
                throw new MoocAuthorizationException($"Forbidden: cannot delete user {id}.");
        }

        await _userService.DeleteAsync(idList);
        return new { };
    }
}

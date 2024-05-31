using UM.Core.Application.DTOs;
using UM.Core.Domain.Models;

namespace UM.Core.Application.Services;

public interface IRoleService
{
    Task<IEnumerable<GetRoleDto>> GetRoles();
    Task<GetRoleDto> GetRoleById(int roleId);
    Task DeleteRole(int id);
    Task<GetRoleDto> CreateRole(GetRoleDto getRoleDto);

}
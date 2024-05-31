using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UM.Core.Application.DTOs;
using UM.Core.Application.Interfaces;
using UM.Core.Application.Services;
using UM.Presentation.WebApi.Models;

namespace UM.Presentation.WebApi.Controllers;
[Route("api/roles")]
[Authorize]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RoleController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetRoleDto>>> GetRoles()
    {
        var roles = await _roleService.GetRoles();

        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    public async Task<ActionResult<GetRoleDto>> GetRoleById(int roleId)
    {
        var role = await _roleService.GetRoleById(roleId);

        return Ok(role);
    }
    
    [HttpPost]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<GetRoleDto>> Post(GetRoleDto getRoleDto)
    {
        var role = await _roleService.CreateRole(getRoleDto);
        
        return Ok(role);
    }

    [HttpDelete]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task DeleteRole(int roleId)
    {
        await _roleService.DeleteRole(roleId);
    }
}
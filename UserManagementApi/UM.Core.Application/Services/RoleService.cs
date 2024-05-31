using AutoMapper;
using UM.Core.Application.DTOs;
using UM.Core.Application.Interfaces;
using UM.Core.Application.Services;
using UM.Core.Domain.Models;

namespace UM.Core.Application;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetRoleDto>> GetRoles()
    {
        var roles = await _unitOfWork.RoleRepository.ReadAsync();

        var mappedRoles = _mapper.Map<IEnumerable<GetRoleDto>>(roles);
        
        return mappedRoles;
    }

    public async Task<GetRoleDto> GetRoleById(int roleId)
    {
        var role = await _unitOfWork.RoleRepository.ReadAsync(roleId);

        var mappedRole = _mapper.Map<GetRoleDto>(role);
        
        return mappedRole;
    }

    public async Task DeleteRole(int id)
    {
        var roleToDelete = await _unitOfWork.RoleRepository.ReadAsync(id);
        _unitOfWork.RoleRepository.Delete(roleToDelete);
        await _unitOfWork.SaveAsync();
    }

    public async Task<GetRoleDto> CreateRole(GetRoleDto roleDto)
    {
        var mappedRole = _mapper.Map<Role>(roleDto);
        _unitOfWork.RoleRepository.Create(mappedRole);
        await _unitOfWork.SaveAsync();
        return roleDto;
    }
}
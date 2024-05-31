using AutoMapper;
using UM.Core.Application.DTOs;
using UM.Core.Domain.Models;
using UM.Presentation.WebApi.Models;

namespace UM.Core.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<GetUserDto, User>();
        CreateMap<GetRoleDto, Role>();
        CreateMap<Role, GetRoleDto>();
        CreateMap<User, UserRequest>();
        CreateMap<UserRequest, User>();
    }
}
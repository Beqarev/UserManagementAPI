using UM.Core.Application.DTOs;
using UM.Core.Domain.Models;
using UM.Presentation.WebApi.Models;

namespace UM.Core.Application.Interfaces;

public interface IUserService
{
    Task<GetUserDto> RegisterUser(UserRequest request);
    Task<IEnumerable<GetUserDto>> GetUsers();
    Task<GetUserDto> GetUserById(int userId);
    Task<GetUserDto> GetUserByUsername(string userName);
    Task DeleteUser(int userId);
    Task<GetUserDto> UpdateUser(int userId, UserRequest request);
}
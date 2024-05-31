using AutoMapper;
using UM.Core.Application.Common;
using UM.Core.Application.DTOs;
using UM.Core.Application.Interfaces;
using UM.Core.Domain.Models;
using UM.Presentation.WebApi.Models;

namespace UM.Core.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<GetUserDto> RegisterUser(UserRequest request)
    {
        //TODO parolis heshireba aris gasaketebeli da validaciebi aris dasadebi
        var role = await _unitOfWork.RoleRepository.ReadAsync(request.roleId);
        
        var passwordHash = Functions.GetPasswordHash(request.Password);
        
        var user = new User
        {
            Id = 0,
            Username = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PrivateNumber = request.PrivateNumber,
            Password = passwordHash,
            Role = role
        };
        await _unitOfWork.UserRepository.CreateUser(user);

        await _unitOfWork.SaveAsync();
        
        var mappedUser = _mapper.Map<GetUserDto>(user);
        
        return mappedUser;
    }

    public async Task<IEnumerable<GetUserDto>> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.ReadAsync();

        var mappedUsers = _mapper.Map<IEnumerable<GetUserDto>>(users);

        return mappedUsers;
    }

    public async Task<GetUserDto> GetUserById(int userId)
    {
        var user = await _unitOfWork.UserRepository.GetUserById(userId);
        var mappedUser = _mapper.Map<GetUserDto>(user);
        return mappedUser;
    }

    public async Task<GetUserDto> GetUserByUsername(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(userName);
        var mappedUser = _mapper.Map<GetUserDto>(user);
        return mappedUser;
    }

    public async Task DeleteUser(int userId)
    {
        var userToDelete = await _unitOfWork.UserRepository.GetUserById(userId);
        _unitOfWork.UserRepository.Delete(userToDelete);
        await _unitOfWork.SaveAsync();
    }

    public async Task<GetUserDto> UpdateUser(int userId, UserRequest request)
    {
        var mappedUser = _mapper.Map<User>(request);
        
        var passwordHash = Functions.GetPasswordHash(mappedUser.Password);
        mappedUser.Password = passwordHash;
        
        await _unitOfWork.UserRepository.UpdateUser(userId, mappedUser);
        var mappedUserDto = _mapper.Map<GetUserDto>(mappedUser);
        await _unitOfWork.SaveAsync();
        return mappedUserDto;
    }
}
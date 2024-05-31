using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UM.Core.Application.DTOs;
using UM.Core.Application.Exceptions;
using UM.Core.Application.Interfaces;
using UM.Core.Application.Validators;
using UM.Presentation.WebApi.Models;

namespace UM.Presentation.WebApi.Controllers;

[Route("api/users")]
[Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
        
    }
    
    [HttpPost]
    public async Task<ActionResult<GetUserDto>> RegisterUser([FromBody] UserRequest request, [FromServices] UserRequestValidator validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var user = await _userService.RegisterUser(request);
        
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers()
    {
        var users = await _userService.GetUsers();

        return Ok(users);
    }

    [HttpGet("userid")]
    public async Task<ActionResult<GetUserDto>> GetUserById(int userId)
    {
        var user = await _userService.GetUserById(userId);

        return Ok(user);
    }

    [HttpGet("username")]
    public async Task<ActionResult<GetUserDto>> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsername(username);

        return Ok(user);
    }

    [HttpPut]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<GetUserDto>> UpdateUser(int userId, [FromBody] UserRequest request)
    {
        var updatedUser = await _userService.UpdateUser(userId, request);
        return Ok(updatedUser);
    }
    
    [HttpDelete]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task DeleteRole(int userId)
    {
        await _userService.DeleteUser(userId);
    }
}
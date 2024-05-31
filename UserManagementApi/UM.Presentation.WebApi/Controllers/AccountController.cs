using MediatR;
using Microsoft.AspNetCore.Mvc;
using UM.Core.Application.DTOs;
using UM.Core.Application.Exceptions;
using UM.Core.Application.Interfaces;
using UM.Core.Application.Services;
using UM.Presentation.WebApi.Extensions.Services;
using UM.Presentation.WebApi.Models;

namespace UM.Presentation.WebApi.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IUserService _userService;


    public AccountsController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    [HttpPost("logIn")]
    public async Task<ActionResult<GetUserDto>> LogIn([FromBody] LoginRequest request,
        [FromServices] IConfiguration configuration)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.GetUserByUsername(request.Username);

        if (user == null)
        {
            throw new UnAuthenticationException("მომხმარებელი ვერ მოიძებნა!");
        }

        var token = JwtAuthenticationExtensions.GenerateJwtToken(
            configuration,
            user.Id.ToString(),
            user.UserName,
            user.Email,
            user.FirstName,
            user.LastName,
            user.PrivateNumber,
            user.roleId
        );

        Response.Headers.Add("AccessToken", token);
        
        return user;

    }

}
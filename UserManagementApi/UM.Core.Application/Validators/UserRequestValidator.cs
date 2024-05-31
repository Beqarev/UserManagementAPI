using FluentValidation;
using UM.Core.Application.Interfaces;
using UM.Presentation.WebApi.Models;

namespace UM.Core.Application.Validators;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    private readonly IUserService _userService;

    public UserRequestValidator(IUserService userService)
    {
        _userService = userService;
        
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("მომხმარებლის სახელი სავალდებულოა.")
            .MustAsync(BeUniqueUsername).WithMessage("მომხმარებელი ამ სახელით უკვე არსებობს.");
        
    }
    
    private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return await _userService.GetUserByUsername(username) == null;
    }
    
}
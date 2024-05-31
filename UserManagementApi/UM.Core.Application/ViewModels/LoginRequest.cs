using System.ComponentModel.DataAnnotations;

namespace UM.Presentation.WebApi.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "მომხმარებლის სახელი სავალდებულოა.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "პაროლი სავალდებულოა.")]
    public string Password { get; set; }
}
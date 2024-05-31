namespace UM.Presentation.WebApi.Models;

public class UserRequest
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PrivateNumber { get; set; }
    public string Password { get; set; }
    public int roleId { get; set; }
}
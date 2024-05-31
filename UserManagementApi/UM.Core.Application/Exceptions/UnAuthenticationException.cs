using System.Net;

namespace UM.Core.Application.Exceptions;

public class UnAuthenticationException : DataValidationException
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public UnAuthenticationException(string message) : base(message) { }
    
}
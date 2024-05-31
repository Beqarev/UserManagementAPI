namespace UM.Core.Application.Exceptions;

public abstract class DataValidationException : Exception
{
    public abstract int StatusCode { get; }

    protected DataValidationException(string message) : base(message) { }
}
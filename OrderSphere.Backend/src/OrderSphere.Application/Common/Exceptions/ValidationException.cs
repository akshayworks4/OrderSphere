namespace OrderSphere.Application.Common.Exceptions;

public sealed class ValidationException : BaseException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors): base("One or more validation failures occurred.")
    {
        Errors = errors;
    }
}
namespace OrderSphere.Application.Common.Exceptions;

public class AuthenticationException : BaseException
{
    protected AuthenticationException(string message) : base(message)
    {
        
    }
}
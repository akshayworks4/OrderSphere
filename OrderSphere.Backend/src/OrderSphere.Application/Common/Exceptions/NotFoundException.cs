namespace OrderSphere.Application.Common.Exceptions;

public sealed class NotFoundException: BaseException
{
    public NotFoundException(string entityName,object id ) : 
        base($"{entityName} with {id} not found.")
    {
    }
}
namespace OrderManagementSystem.Application.Exceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
}
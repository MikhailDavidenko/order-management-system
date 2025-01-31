namespace OrderManagementSystem.Application.Accounting;

public sealed record UpdateUserCommand(
    string Email,
    string Password,
    string UserName,
    Guid CustomerId);
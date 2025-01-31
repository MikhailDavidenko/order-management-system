namespace OrderManagementSystem.Application.Accounting;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string UserName,
    Guid CustomerId);
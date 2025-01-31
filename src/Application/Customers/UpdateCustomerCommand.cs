namespace OrderManagementSystem.Application.Customers;

public sealed record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string Code,
    string Address,
    decimal? Discount);
namespace OrderManagementSystem.Application.Customers;

public sealed record CreateCustomerCommand(
    string Name,
    string Code,
    string Address,
    decimal? Discount);
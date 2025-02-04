namespace OrderManagementSystem.Application.Customers;

public sealed record CreateCustomerCommand(
    string Name,
    string Address,
    decimal? Discount);
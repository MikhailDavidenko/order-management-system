namespace OrderManagementSystem.Application.Orders;

public record CreateOrderItemCommand(
    Guid ProductId,
    int Quantity);
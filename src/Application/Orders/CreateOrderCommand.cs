namespace OrderManagementSystem.Application.Orders;

public record CreateOrderCommand(
    Guid CustomerId,
    List<CreateOrderItemCommand> OrderItems);
namespace OrderManagementSystem.Contracts.Orders;

public sealed class CreateOrderRequest
{
    public IReadOnlyList<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
}
namespace OrderManagementSystem.Contracts.Orders;

public sealed class OrderItemRequest
{
    public Guid? ProductId { get; set; }
    
    public int? Quantity { get; set; }
}
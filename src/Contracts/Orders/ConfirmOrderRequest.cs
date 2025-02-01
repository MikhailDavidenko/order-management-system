namespace OrderManagementSystem.Contracts.Orders;

public sealed class ConfirmOrderRequest
{
    public DateTime? ShipmentDate { get; set; }
}
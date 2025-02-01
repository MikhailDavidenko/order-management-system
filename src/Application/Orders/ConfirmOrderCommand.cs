namespace OrderManagementSystem.Application.Orders;

public sealed record ConfirmOrderCommand(
    Guid Id,
    DateTime ShipmentDate);
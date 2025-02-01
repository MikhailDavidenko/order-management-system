namespace OrderManagementSystem.Contracts.Orders;

public sealed class OrderResponse
{
    /// <summary>
    ///     Идентификатор заказа.
    /// </summary>
    public Guid? Id { get; init; }
    
    /// <summary>
    ///     Дата когда сделан заказ.
    /// </summary>
    public DateTime? OrderDate { get; init; }

    /// <summary>
    ///     Дата доставки.
    /// </summary>
    public DateTime? ShipmentDate { get; init; }

    /// <summary>
    ///     Номер заказа.
    /// </summary>
    public int? OrderNumber { get; init; }

    /// <summary>
    ///     Состояние заказа.
    /// </summary>
    public string? Status { get; init; }
    
    /// <summary>
    ///     Сумма заказа.
    /// </summary>
    public decimal? TotalPrice { get; init; }
}
namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Заказа.
/// </summary>
public sealed class Order
{
    /// <summary>
    ///     Идентификатор заказа.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     ИД заказчика.
    /// </summary>
    public Guid CustomerId { get; }

    /// <summary>
    ///     Дата когда сделан заказ.
    /// </summary>
    public DateTime OrderDate { get; init; }

    /// <summary>
    ///     Дата доставки.
    /// </summary>
    public DateTime? ShipmentDate { get; }

    /// <summary>
    ///     Номер заказа.
    /// </summary>
    public int OrderNumber { get; }

    /// <summary>
    ///     Состояние заказа.
    /// </summary>
    public OrderStatus Status { get; }
}
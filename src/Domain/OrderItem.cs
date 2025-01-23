namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Элемента заказа.
/// </summary>
public sealed class OrderItem
{
    /// <summary>
    ///     Идентификатор Элемента заказа.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     ИД заказа.
    /// </summary>
    public Guid OrderId { get; init; }

    /// <summary>
    ///     ИД товара.
    /// </summary>
    public Guid ItemId { get; init; }

    /// <summary>
    ///     Количество заказанного товара.
    /// </summary>
    public int ItemsCount { get; }

    /// <summary>
    ///     Цена за единицу.
    /// </summary>
    public decimal ItemPrice { get; }
}
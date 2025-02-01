using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Элемента заказа.
/// </summary>
public sealed class OrderItem
{
    private OrderItem(Guid id, Guid orderId, Guid itemId, int itemsCount, decimal itemPrice)
    {
        Id = id;
        OrderId = orderId;
        ItemId = itemId;
        ItemsCount = itemsCount;
        ItemPrice = itemPrice;
    }
    
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
    public int ItemsCount { get; private set; }

    /// <summary>
    ///     Цена за единицу.
    /// </summary>
    public decimal ItemPrice { get; private set; }
    
    public static OrderItem Create(Guid orderId, Guid itemId, int itemsCount, decimal itemPrice)
    {
        if (itemsCount <= 0)
        {
            throw new ValidationException("Количество товара не может быть отрицательным или равным нулю");
        }
        
        if (itemPrice <= 0)
        {
            throw new ValidationException("Цена не может быть отрицательной или равной нулю");
        }
        
        return new OrderItem(Guid.NewGuid(), orderId, itemId, itemsCount, itemPrice);
    }
}
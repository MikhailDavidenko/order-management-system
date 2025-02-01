using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Заказа.
/// </summary>
public sealed class Order
{
    private Order(Guid id, Guid customerId, DateTime orderDate, DateTime? shipmentDate, int orderNumber, OrderStatus status)
    {
        Id = id;
        CustomerId = customerId;
        OrderDate = orderDate.ToUniversalTime();
        ShipmentDate = shipmentDate?.ToUniversalTime();
        OrderNumber = orderNumber;
        Status = status;
    }
    
    /// <summary>
    ///     Идентификатор заказа.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     ИД заказчика.
    /// </summary>
    public Guid CustomerId { get; init; }

    /// <summary>
    ///     Дата когда сделан заказ.
    /// </summary>
    public DateTime OrderDate { get; init; }

    /// <summary>
    ///     Дата доставки.
    /// </summary>
    public DateTime? ShipmentDate { get; private set; }

    /// <summary>
    ///     Номер заказа.
    /// </summary>
    public int OrderNumber { get; private set; }

    /// <summary>
    ///     Состояние заказа.
    /// </summary>
    public OrderStatus Status { get; private set; }
    
    public List<OrderItem>? OrderItems { get; set; }

    public static Order Create(Guid customerId, int orderNumber)
    {
        return new Order(Guid.NewGuid(), customerId, DateTime.UtcNow, null, orderNumber, OrderStatus.New);
    }

    public void UpdateStatus(OrderStatus nextStatus)
    {
        if (Status == nextStatus)
        {
            return;
        }
        
        var availableStatuses = GetAvailableOrderStatuses(Status);
        
        if(availableStatuses.Contains(nextStatus) == false)
            throw new ValidationException($"Невозможно перевести заказ в состояние {nextStatus} из состояния {Status}");

        Status = nextStatus;
    }
    
    public void Update(DateTime shipmentDate)
    {
        if (shipmentDate < OrderDate)
        {
            throw new ValidationException("Дата доставки не может быть раньше даты заказа");
        }
        
        ShipmentDate = shipmentDate.ToUniversalTime();
    }

    public static List<OrderStatus> GetAvailableOrderStatuses(OrderStatus currentStatus)
    {
        var availableStatuses = new List<OrderStatus>();

        switch (currentStatus)
        {
            case OrderStatus.New:
                availableStatuses.Add(OrderStatus.InProgress);
                break;

            case OrderStatus.InProgress:
                availableStatuses.Add(OrderStatus.Completed);
                break;
            case OrderStatus.Completed:
                break;
        }

        return availableStatuses;
    }
}
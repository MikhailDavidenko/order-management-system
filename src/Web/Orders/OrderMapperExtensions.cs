using OrderManagementSystem.Contracts.Orders;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Web.Orders;

public static class OrderMapperExtensions
{
    public static OrderResponse MapToOrderResponse(this Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            ShipmentDate = order.ShipmentDate,
            OrderNumber = order.OrderNumber,
            Status = order.Status.ToString(),
            TotalPrice = order.OrderItems?.Sum(x => x.ItemPrice * x.ItemsCount)
        };
    }
    
}
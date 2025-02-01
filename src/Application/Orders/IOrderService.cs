using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Orders;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(Guid orderId, Guid? customerId = null, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Order>> GetOrdersAsync(int limit, int offset, Guid? customerId = null, CancellationToken cancellationToken = default);
    
    Task<Order> CreateOrderAsync(CreateOrderCommand order, CancellationToken cancellationToken = default);
    
    Task<Order> ConfirmOrderAsync(ConfirmOrderCommand command, CancellationToken cancellationToken = default);
    
    Task<Order> CloseOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    
    Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}
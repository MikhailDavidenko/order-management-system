using System.Linq.Expressions;
using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Orders;

public interface IOrderRepository : IRepository<Order>
{
    int GetNextOrderNumber();
    
    Task<IReadOnlyList<Order>> GetAllWithItemsAsync(
        Expression<Func<Order, bool>> predicate,
        int limit,
        int offset,
        CancellationToken cancellationToken = default);

    Task<Order?> GetOrderWithItemsAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken);
    
    Task<int> GetOrdersCountAsync(CancellationToken cancellationToken);
}
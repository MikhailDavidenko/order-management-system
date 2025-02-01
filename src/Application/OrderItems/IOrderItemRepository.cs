using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.OrderItems;

public interface IOrderItemRepository : IRepository<OrderItem>
{
    Task AddRangeAsync(IReadOnlyList<OrderItem> entities, CancellationToken cancellationToken);
}
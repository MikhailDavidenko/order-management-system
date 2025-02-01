using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Application.Orders;
using OrderManagementSystem.Data.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.Orders;

internal sealed class OrderRepository : DefaultRepository<Order>, IOrderRepository
{
    private readonly AppDbContext context;
    
    public OrderRepository(AppDbContext context)
        : base(context)
    {
        this.context = context;
    }
    
    public int GetNextOrderNumber()
    {
        var maxOrderNumber = context.Orders
            .Select(o => o.OrderNumber)
            .AsEnumerable()
            .DefaultIfEmpty(0)
            .Max();

        return maxOrderNumber + 1;
    }
    
    public Task<Order?> GetOrderWithItemsAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken)
    {
        return context.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
    
    public async Task<IReadOnlyList<Order>> GetAllWithItemsAsync(
        Expression<Func<Order, bool>> predicate,
        int limit,
        int offset,
        CancellationToken cancellationToken)
    {
        return await context.Orders
            .Include(x => x.OrderItems)
            .Where(predicate)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}
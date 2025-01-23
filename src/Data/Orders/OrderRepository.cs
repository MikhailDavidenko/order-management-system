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
}
using OrderManagementSystem.Application.OrderItems;
using OrderManagementSystem.Data.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.OrderItems;

internal sealed class OrderItemRepository : DefaultRepository<OrderItem>, IOrderItemRepository
{
    private readonly AppDbContext context;
    
    public OrderItemRepository(AppDbContext context)
        : base(context)
    {
        this.context = context;
    }
}
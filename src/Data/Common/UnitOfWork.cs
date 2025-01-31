using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Application.OrderItems;
using OrderManagementSystem.Application.Orders;
using OrderManagementSystem.Application.Products;
using OrderManagementSystem.Data.Customers;
using OrderManagementSystem.Data.OrderItems;
using OrderManagementSystem.Data.Orders;
using OrderManagementSystem.Data.Products;

namespace OrderManagementSystem.Data.Common;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Customers = new CustomerRepository(context);
        Orders = new OrderRepository(context);
        OrderItems = new OrderItemRepository(context);
        Products = new ProductRepository(context);
    }

    public ICustomerRepository Customers { get; private set; }
    
    public IOrderRepository Orders { get; private set; }
    
    public IOrderItemRepository OrderItems { get; private set; }
    
    public IProductRepository Products { get; private set; }

    public int Complete()
    {
        return context.SaveChanges();
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
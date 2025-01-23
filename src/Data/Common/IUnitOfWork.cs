using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Application.OrderItems;
using OrderManagementSystem.Application.Orders;
using OrderManagementSystem.Application.Products;

namespace OrderManagementSystem.Data.Common;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    IProductRepository Products { get; }
    
    int Complete();
    
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
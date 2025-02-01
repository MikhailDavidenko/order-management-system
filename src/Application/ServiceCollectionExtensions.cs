using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Application.Accounting;
using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Application.Orders;
using OrderManagementSystem.Application.Products;

namespace OrderManagementSystem.Application;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует бизнес-сервисы
    /// </summary>
    public static IServiceCollection AddBusinessServices(
        this IServiceCollection services)
        => services
            .AddTransient<IUserService, UserService>()
            .AddTransient<IOrderService, OrderService>()
            .AddTransient<IProductService, ProductService>()
            .AddTransient<ICustomerService, CustomerService>();
}
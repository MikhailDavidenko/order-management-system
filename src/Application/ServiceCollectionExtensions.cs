using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Application.Accounting;
using OrderManagementSystem.Application.Customers;

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
            /*.AddTransient<IOrderService, OrderService>()
            .AddTransient<IOrderItemService, OrderItemService>()
            .AddTransient<IProductService, ProductService>()*/
            .AddTransient<ICustomerService, CustomerService>();
}
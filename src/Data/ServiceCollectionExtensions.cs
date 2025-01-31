using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Application.OrderItems;
using OrderManagementSystem.Application.Orders;
using OrderManagementSystem.Application.Products;
using OrderManagementSystem.Data.Common;
using OrderManagementSystem.Data.Customers;
using OrderManagementSystem.Data.OrderItems;
using OrderManagementSystem.Data.Orders;
using OrderManagementSystem.Data.Products;

namespace OrderManagementSystem.Data;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует UnitOfWork
    /// </summary>
    public static IServiceCollection AddUnitOfWork(
        this IServiceCollection services)
        => services
            .AddScoped<IUnitOfWork, UnitOfWork>();
    
    /// <summary>
    /// Регистрирует репозитории
    /// </summary>
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
        => services
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IOrderItemRepository, OrderItemRepository>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<ICustomerRepository, CustomerRepository>();

    /// <summary>
    /// Регистрирует контекст БД
    /// </summary>
    public static IServiceCollection AddContext(
        this IServiceCollection services)
    {
        services
        .AddOptions<DbConnectionOptions>()
        .BindConfiguration(DbConnectionOptions.OptionsKey);

        services
            .AddDbContext<AppDbContext>((provider, builder) =>
            {
                var connectionOptions = provider.GetRequiredService<IOptions<DbConnectionOptions>>();

                builder.UseNpgsql(connectionOptions.Value.RequiredConnectionString,
                    b => b.MigrationsAssembly("Data"));
            });
        
        return services;
    }
}
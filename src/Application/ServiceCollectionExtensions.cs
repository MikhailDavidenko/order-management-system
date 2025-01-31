using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Application.Accounting;

namespace OrderManagementSystem.Application;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует бизнес-сервисы
    /// </summary>
    public static IServiceCollection AddBusinessServices(
        this IServiceCollection services)
        => services
            .AddTransient<IUserService, UserService>();
}
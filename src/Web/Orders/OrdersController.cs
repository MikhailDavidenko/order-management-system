using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Exceptions;
using OrderManagementSystem.Application.Orders;
using OrderManagementSystem.Contracts.Orders;
using OrderManagementSystem.Domain;
using OrderManagementSystem.Web.Accounting;

namespace OrderManagementSystem.Web.Orders;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly UserManager<ApplicationUser> userManager;

    public OrdersController(UserManager<ApplicationUser> userManager, IOrderService orderService)
    {
        this.userManager = userManager;
        this.orderService = orderService;
    }
    
    [Authorize(Policy = ApplicationRoleNames.AllowAnyPolicy)]
    [HttpGet]
    public async Task<IReadOnlyList<OrderResponse>> GetOrdersAsync(
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        [FromQuery] OrderStatus? status,
        CancellationToken cancellationToken)
    {
        if (offset < 0 || limit < 1)
        {
            throw new ArgumentException($"Параметр offset не должен быть меньше нуля, limit должен быть больше нуля");
        }

        var customerId = await GetCustomerIdAsync();

        var orders = await orderService.GetOrdersAsync(limit ?? 10, offset ?? 0, status, customerId, cancellationToken);
        
        var ordersResponses = orders
            .Select(x => x.MapToOrderResponse())
            .ToList();
        
        return ordersResponses;
    }
    
    [Authorize(Policy = ApplicationRoleNames.AllowAnyPolicy)]
    [HttpGet("count")]
    public async Task<int> GetOrdersTotalCountAsync(CancellationToken cancellationToken)
    {
        var count = await orderService.GetOrdersCountAsync(cancellationToken);
        
        return count;
    }
    
    [Authorize(Policy = ApplicationRoleNames.AllowAnyPolicy)]
    [HttpGet("{orderId}")]
    public async Task<OrderResponse> GetOrderByIdAsync(
        [FromRoute] Guid orderId,
        CancellationToken cancellationToken)
    {
        var customerId = await GetCustomerIdAsync();
        
        var order = await orderService.GetOrderByIdAsync(orderId, customerId, cancellationToken);
        
        return order.MapToOrderResponse();
    }
    
    [Authorize(Roles = ApplicationRoleNames.Customer)]
    [HttpPost]
    public async Task<OrderResponse> CreateOrderAsync(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var customerId = await GetCustomerIdAsync();
        
        var command = new CreateOrderCommand
        (
            customerId.Value,
            request.Items
                .Select(x => new CreateOrderItemCommand(x.ProductId.Value, x.Quantity.Value))
                .ToList()
        );
        
        var order = await orderService.CreateOrderAsync(command, cancellationToken);
        
        return order.MapToOrderResponse();
    }
    
    [Authorize(Roles = ApplicationRoleNames.Manager)]
    [HttpPut("{orderId}/confirm")]
    public async  Task<OrderResponse> ConfirmOrderAsync(
        [FromRoute] Guid orderId,
        [FromBody] ConfirmOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ConfirmOrderCommand
        (
            orderId,
            request.ShipmentDate.Value
        );
        
        var order = await orderService.ConfirmOrderAsync(command, cancellationToken);
        
        return order.MapToOrderResponse();
    }
    
    [Authorize(Roles = ApplicationRoleNames.Manager)]
    [HttpPut("{orderId}/close")]
    public async  Task<OrderResponse> CloseOrderAsync(
        [FromRoute] Guid orderId,
        CancellationToken cancellationToken)
    {
        var order = await orderService.CloseOrderAsync(orderId, cancellationToken);
        
        return order.MapToOrderResponse();
    }
    
    [Authorize(Roles = ApplicationRoleNames.Customer)]
    [HttpDelete("{orderId}")]
    public async Task DeleteOrderAsync(
        [FromRoute] Guid orderId,
        CancellationToken cancellationToken)
    {
        await orderService.DeleteOrderAsync(orderId, cancellationToken);
    }

    private async Task<Guid?> GetCustomerIdAsync()
    {
        if(!User.IsInRole(ApplicationRoleNames.Manager))
        {
            var user = await userManager.GetUserAsync(User) ?? throw new EntityNotFoundException("Пользователь не найден");
            
            if (user.CustomerId == null)
            {
                throw new ArgumentException($"У пользователя с ролью Заказчик не может отсутствовать {nameof(user.CustomerId)}");
            }
            
            return user.CustomerId.Value;
        }

        return null;
    }
}
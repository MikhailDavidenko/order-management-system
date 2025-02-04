using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Application.Exceptions;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Orders;

public sealed class OrderService : IOrderService
{
    private readonly IUnitOfWork unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
        => unitOfWork.Orders.GetOrdersCountAsync(cancellationToken);
    
    public async Task<Order> GetOrderByIdAsync(Guid orderId, Guid? customerId, CancellationToken cancellationToken)
    {
        Expression<Func<Order, bool>> predicate = customerId is not null 
            ? x => x.Id == orderId && x.CustomerId == customerId.Value
            : x => x.Id == orderId;

        var order = await unitOfWork.Orders.GetOrderWithItemsAsync(predicate, cancellationToken);

        return order 
               ?? throw new EntityNotFoundException($"Заказ {orderId} не найден");
    }

    public async Task<IReadOnlyList<Order>> GetOrdersAsync(
        int limit,
        int offset,
        OrderStatus? status = null,
        Guid? customerId = null,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<Order, bool>> predicate = customerId is not null
            ? x => x.CustomerId == customerId.Value
            : x => true;

        if (status is not null)
        {
            predicate = predicate.And(x => x.Status == status.Value);
            
        }

        var orders = await unitOfWork.Orders.GetAllWithItemsAsync(predicate, limit, offset, cancellationToken);
        
        return orders;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        var customer = await unitOfWork.Customers.GetFirstOrDefaultAsync(c => c.Id == command.CustomerId, cancellationToken);
        
        if (customer is null)
        {
            throw new EntityNotFoundException($"Заказчик {command.CustomerId} не найден");
        }
        
        var orderNumber = unitOfWork.Orders.GetNextOrderNumber();
        var newOrder = Order.Create(command.CustomerId, orderNumber);
        
        var products = await unitOfWork.Products.GetAllWithWhereAsync(
            product => command.OrderItems.Select(
                p => p.ProductId)
                .Contains(product.Id), cancellationToken);

        if (products.Count != command.OrderItems.Count)
        {
            throw new EntityNotFoundException("Некоторые товары не найдены");
        }
        
        decimal discount = customer.Discount is null or 0 
            ? 1 :  (1 - customer.Discount.Value / 100);
        
        var orderItems = command.OrderItems
            .Join(products,
                orderItem => orderItem.ProductId,
                product => product.Id,
                (orderItem, product) => new { orderItem, product })
            .Select(x => OrderItem.Create(
                newOrder.Id,
                x.orderItem.ProductId,
                x.orderItem.Quantity,
                x.product.Price * discount))
            .ToList();
        
        newOrder.OrderItems = orderItems;

        await unitOfWork.Orders.AddAsync(newOrder, cancellationToken);
        await unitOfWork.OrderItems.AddRangeAsync(orderItems, cancellationToken);
        
        await unitOfWork.CompleteAsync(cancellationToken);
        
        return newOrder;
    }

    public async Task<Order> ConfirmOrderAsync(ConfirmOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = await unitOfWork.Orders.GetFirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
        
        if (order is null)
        {
            throw new EntityNotFoundException($"Заказ {command.Id} не найден");
        }
        
        order.Update(command.ShipmentDate);
        
        order.UpdateStatus(OrderStatus.InProgress);
        
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        return order;
    }

    public async Task<Order> CloseOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await unitOfWork.Orders.GetFirstOrDefaultAsync(c => c.Id == orderId, cancellationToken);

        if (order is null)
        {
            throw new EntityNotFoundException($"Заказ {orderId} не найден");
        }

        order.UpdateStatus(OrderStatus.Completed);
        
        unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        return order;
    }

    public async Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await unitOfWork.Orders.GetFirstOrDefaultAsync(c => c.Id == orderId, cancellationToken);
        
        if (order is not null)
        {
            if(order.Status != OrderStatus.New)
                throw new ValidationException($"Невозможно удалить заказ в состоянии {order.Status}");
            
            unitOfWork.Orders.Remove(order);

            await unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}
using FluentValidation;
using OrderManagementSystem.Contracts.Orders;

namespace OrderManagementSystem.Web.Orders.Validators;

public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
{
    public OrderItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
    }
}
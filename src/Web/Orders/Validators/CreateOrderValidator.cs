using FluentValidation;
using OrderManagementSystem.Contracts.Orders;

namespace OrderManagementSystem.Web.Orders.Validators;

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Items).NotNull();
        RuleFor(x => x.Items.Count).GreaterThan(0);
        RuleForEach(x => x.Items).SetValidator(new OrderItemRequestValidator());
    }
}
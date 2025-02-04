using FluentValidation;
using OrderManagementSystem.Contracts.Customers;

namespace OrderManagementSystem.Web.Customers.Validators;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
    }
}
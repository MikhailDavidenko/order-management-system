using FluentValidation;
using OrderManagementSystem.Contracts.Customers;

namespace OrderManagementSystem.Web.Customers.Validators;

public sealed class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
    }
}
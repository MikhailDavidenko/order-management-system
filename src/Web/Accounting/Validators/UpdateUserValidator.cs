using FluentValidation;
using OrderManagementSystem.Contracts.Accounting;

namespace OrderManagementSystem.Web.Accounting.Validators;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
using FluentValidation;
using OrderManagementSystem.Contracts.Accounting;

namespace OrderManagementSystem.Web.Accounting.Validators;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().MinimumLength(8);
    }
}
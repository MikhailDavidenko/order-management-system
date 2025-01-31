using FluentValidation;
using OrderManagementSystem.Contracts.Products;

namespace OrderManagementSystem.Web.Products.Validators;

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Price).NotNull().GreaterThan(0);
    }
}
using crud_curotec.Domain.Entities;
using FluentValidation;

namespace crud_curotec.Application.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().NotNull()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name must be at most 100 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");
        }
    }
}

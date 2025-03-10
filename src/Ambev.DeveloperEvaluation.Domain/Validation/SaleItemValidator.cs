using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for the SaleItem entity.
    /// Validates the product identifier, quantity, unit price, and ensures that the discount complies with business rules.
    /// </summary>
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.Product)
                .NotEmpty().WithMessage("Product cannot be empty.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("Quantity cannot be greater than 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

            RuleFor(item => item)
                .Custom((item, context) =>
                {
                    if (item.Quantity < 4 && item.Discount != 0)
                    {
                        context.AddFailure("Discount", "Discount must be 0 for quantities below 4.");
                    }
                    else if (item.Quantity >= 4 && item.Quantity < 10 && item.Discount != 0.10m)
                    {
                        context.AddFailure("Discount", "Discount must be 10% for quantities between 4 and 9.");
                    }
                    else if (item.Quantity >= 10 && item.Quantity <= 20 && item.Discount != 0.20m)
                    {
                        context.AddFailure("Discount", "Discount must be 20% for quantities between 10 and 20.");
                    }
                });
        }
    }
}

using System;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for the Sale entity.
    /// Validates the sale number, sale date, customer, branch, and ensures at least one valid sale item is present.
    /// </summary>
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number cannot be empty.");

            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.");

            RuleFor(sale => sale.Items)
                .NotNull().WithMessage("Sale items cannot be null.")
                .Must(items => items.Any())
                .WithMessage("Sale must have at least one item.");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemValidator());
        }
    }
}

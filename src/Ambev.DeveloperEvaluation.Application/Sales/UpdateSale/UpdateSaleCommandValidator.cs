using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for updating a sale.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Required (cannot be an empty Guid).
    /// - SaleNumber: Required and must be between 3 and 50 characters.
    /// - SaleDate: Must not be in the future.
    /// - Customer: Required.
    /// - Branch: Required.
    /// - Items: The sale must have at least one item.
    /// - Each SaleItemDto is validated using the <see cref="SaleItemDtoValidator"/>.
    /// </remarks>
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(sale => sale.Id)
                .NotEmpty().WithMessage("Sale Id is required for update.");

            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.")
                .Length(3, 50).WithMessage("Sale number must be between 3 and 50 characters.");

            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date cannot be in the future.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("Sale must have at least one item.");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemDtoValidator());
        }
    }
}

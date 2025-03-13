using Ambev.DeveloperEvaluation.Common.DTO;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand that defines validation rules for the sale creation command.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleNumber: Required and must be between 3 and 50 characters.
    /// - SaleDate: Must not be in the future.
    /// - Customer: Required.
    /// - Branch: Required.
    /// - Items: The sale must have at least one item.
    /// - Each SaleItemDto is validated using SaleItemDtoValidator.
    /// </remarks>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        public CreateSaleCommandValidator()
        {
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

    /// <summary>
    /// Validator for the SaleItemDto used in the CreateSaleCommand.
    /// </summary>
    public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the SaleItemDtoValidator with defined validation rules.
        /// </summary>
        public SaleItemDtoValidator()
        {
            RuleFor(item => item.Product)
                .NotEqual(Guid.Empty).WithMessage("Product identifier must be valid.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}

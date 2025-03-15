using System;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleRequest that defines validation rules for creating a new sale.
    /// </summary>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleRequestValidator"/> with defined validation rules.
        /// </summary>
        public CreateSaleRequestValidator()
        {
            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date cannot be in the future.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer identifier must be provided.")
                .Must(product => product != Guid.Empty)
                .WithMessage("Customer identifier must be a valid GUID.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch identifier must be provided.")
                .Must(product => product != Guid.Empty)
                .WithMessage("Branch identifier must be a valid GUID.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("Sale must have at least one item.");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemRequestValidator());
        }
    }

    /// <summary>
    /// Validator for the SaleItemRequest used in the CreateSaleRequest.
    /// </summary>
    public class SaleItemRequestValidator : AbstractValidator<SaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItemRequestValidator"/> with defined validation rules.
        /// </summary>
        public SaleItemRequestValidator()
        {
            RuleFor(item => item.Product)
                .NotEmpty().WithMessage("Product identifier must be provided.")
                .Must(product => product != Guid.Empty)
                .WithMessage("Product identifier must be a valid GUID.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}

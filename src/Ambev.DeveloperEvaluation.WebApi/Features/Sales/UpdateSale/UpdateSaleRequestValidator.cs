using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleRequest that defines validation rules for updating a sale.
    /// </summary>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleRequestValidator"/>.
        /// </summary>
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale.Id)
                .NotEmpty().WithMessage("Sale ID is required.");

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
    /// Validator for the SaleItemRequest used in the UpdateSaleRequest.
    /// </summary>
    public class SaleItemRequestValidator : AbstractValidator<SaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItemRequestValidator"/>.
        /// </summary>
        public SaleItemRequestValidator()
        {
            RuleFor(item => item.Product)
                .NotEqual(Guid.Empty).WithMessage("Product identifier must be a valid GUID.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}

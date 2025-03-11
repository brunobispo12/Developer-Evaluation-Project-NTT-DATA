using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Validator for <see cref="DeleteSaleCommand"/> ensuring that the sale ID is valid.
    /// </summary>
    public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteSaleCommandValidator"/>.
        /// </summary>
        public DeleteSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID must not be empty.");
        }
    }
}

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Validator for <see cref="GetSaleByIdCommand"/>, ensuring the sale ID is valid.
    /// </summary>
    public class GetSaleByIdCommandValidator : AbstractValidator<GetSaleByIdCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleByIdCommandValidator"/>.
        /// </summary>
        public GetSaleByIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID must not be empty.");
        }
    }
}

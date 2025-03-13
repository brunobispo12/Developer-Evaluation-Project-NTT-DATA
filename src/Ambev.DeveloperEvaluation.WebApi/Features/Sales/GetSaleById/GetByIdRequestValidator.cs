using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    /// <summary>
    /// Validator for <see cref="GetSaleByIdRequest"/> that ensures the sale ID is provided.
    /// </summary>
    public class GetSaleByIdRequestValidator : AbstractValidator<GetSaleByIdRequest>
    {
        public GetSaleByIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID must not be empty.");
        }
    }
}

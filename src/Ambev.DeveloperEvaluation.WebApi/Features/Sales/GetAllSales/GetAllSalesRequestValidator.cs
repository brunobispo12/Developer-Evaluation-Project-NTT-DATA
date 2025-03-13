using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales
{
    /// <summary>
    /// Validator for GetAllSalesRequest that defines rules for pagination and ordering.
    /// </summary>
    public class GetAllSalesRequestValidator : AbstractValidator<GetAllSalesRequest>
    {
        public GetAllSalesRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than zero.");

            RuleFor(x => x.Order)
                .Must(BeAValidOrder).When(x => !string.IsNullOrWhiteSpace(x.Order))
                .WithMessage("Invalid sorting criteria.");
        }

        private bool BeAValidOrder(string? order)
        {
            if (string.IsNullOrWhiteSpace(order))
                return true;
            string trimmed = order.Trim().ToLower();
            return trimmed.EndsWith(" asc") || trimmed.EndsWith(" desc");
        }
    }
}

using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using FluentValidation;

/// <summary>
/// Validator for GetAllSalesCommand, ensuring that pagination and sorting are valid.
/// </summary>
public class GetAllSalesCommandValidator : AbstractValidator<GetAllSalesCommand>
{
    /// <summary>
    /// Configures validation rules for PageNumber, PageSize, and Order.
    /// </summary>
    public GetAllSalesCommandValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.");

        RuleFor(x => x.Order)
            .Must(BeAValidOrder).When(x => !string.IsNullOrEmpty(x.Order))
            .WithMessage("Invalid sorting criteria.");
    }

    /// <summary>
    /// Checks if the dynamic ordering string is valid.
    /// </summary>
    /// <param name="order">Ordering expression to validate.</param>
    /// <returns>true if the order is valid or not provided; otherwise, false.</returns>
    private bool BeAValidOrder(string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return true;

        string trimmed = order.Trim().ToLower();
        bool endsWithAscDesc = trimmed.EndsWith(" asc") || trimmed.EndsWith(" desc");
        return endsWithAscDesc;
    }
}
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    /// <summary>
    /// Command for retrieving all sales with pagination and ordering.
    /// </summary>
    /// <remarks>
    /// This command captures the necessary parameters for retrieving a paginated list of sales, including:
    /// - Page number
    /// - Page size
    /// - Ordering (e.g., "SaleDate desc, SaleNumber asc")
    /// 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request that returns a <see cref="GetAllSalesResult"/>.
    /// </remarks>
    public class GetAllSalesCommand : IRequest<GetAllSalesResult>
    {
        /// <summary>
        /// Gets or sets the page number (starting at 1).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the ordering string (e.g., "SaleDate desc, SaleNumber asc").
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// Validates the command using the GetAllSalesCommandValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <c>IsValid</c>: True if all validation rules pass.
        /// - <c>Errors</c>: A collection of validation errors if any rules fail.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new GetAllSalesCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

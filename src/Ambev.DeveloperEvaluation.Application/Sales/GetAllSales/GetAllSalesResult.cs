using Ambev.DeveloperEvaluation.Common.DTO;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    /// <summary>
    /// Represents the result returned after retrieving all sales.
    /// </summary>
    /// <remarks>
    /// This result contains a collection of sale DTOs along with pagination metadata.
    /// </remarks>
    public class GetAllSalesResult
    {
        /// <summary>
        /// Gets or sets the collection of sale DTOs.
        /// </summary>
        public List<SaleDto> Sales { get; set; } = new List<SaleDto>();

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page size (items per page).
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total count of sales.
        /// </summary>
        public int TotalCount { get; set; }
    }
}

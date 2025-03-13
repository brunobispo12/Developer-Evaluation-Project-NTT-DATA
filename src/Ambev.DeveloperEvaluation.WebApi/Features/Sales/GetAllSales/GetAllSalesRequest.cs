using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales
{
    /// <summary>
    /// Represents a request to retrieve all sales with pagination and ordering.
    /// </summary>
    public class GetAllSalesRequest
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
    }
}

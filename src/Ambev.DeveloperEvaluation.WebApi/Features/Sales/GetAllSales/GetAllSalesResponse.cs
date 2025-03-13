using Ambev.DeveloperEvaluation.Common.DTO;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales
{
    /// <summary>
    /// API response model for the GetAllSales operation.
    /// </summary>
    public class GetAllSalesResponse
    {
        /// <summary>
        /// Gets or sets the collection of Sale DTOs.
        /// </summary>
        public List<SaleDto> Sales { get; set; } = new List<SaleDto>();

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page size (number of items per page).
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

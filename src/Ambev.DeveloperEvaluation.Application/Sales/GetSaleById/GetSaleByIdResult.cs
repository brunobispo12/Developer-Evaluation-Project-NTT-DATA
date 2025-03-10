using Ambev.DeveloperEvaluation.Common.DTO;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Represents the response returned after retrieving a sale by ID.
    /// </summary>
    /// <remarks>
    /// This result contains the details of the sale.
    /// </remarks>
    public class GetSaleByIdResult
    {
        /// <summary>
        /// The unique identifier of the sale.
        /// </summary>
        public SaleDto? Sale { get; set; }
    }
}

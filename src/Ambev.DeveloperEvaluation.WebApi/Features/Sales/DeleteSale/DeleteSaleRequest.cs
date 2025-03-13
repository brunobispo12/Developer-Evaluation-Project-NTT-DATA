using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    /// <summary>
    /// Represents a request to delete a new sale in the system.
    /// </summary>
    public class DeleteSaleRequest
    {
        /// <summary>
        /// The unique identifier of the sale to delete
        /// </summary>
        public Guid Id { get; set; } = Guid.Empty;

    }
}

using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents the response returned after successfully updating an existing sale.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the updated sale,
    /// which can be used for subsequent operations or reference.
    /// </remarks>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated sale.
        /// </summary>
        public Guid Id { get; set; }
    }
}

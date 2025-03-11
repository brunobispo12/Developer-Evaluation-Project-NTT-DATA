using Ambev.DeveloperEvaluation.Common.DTO;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents the response returned after successfully updating an existing sale.
    /// </summary>
    /// <remarks>
    /// This response contains the updated sale details as a SaleDto,
    /// which can be used for subsequent operations or reference.
    /// </remarks>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the updated sale details.
        /// </summary>
        public SaleDto Sale { get; set; } = new SaleDto();
    }
}

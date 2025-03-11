using Ambev.DeveloperEvaluation.Common.DTO;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Represents the response returned after successfully creating a new sale.
    /// </summary>
    /// <remarks>
    /// This response contains the complete SaleDto with all sale details.
    /// </remarks>
    public class CreateSaleResult
    {
        /// <summary>
        /// Gets or sets the created sale details.
        /// </summary>
        public SaleDto Sale { get; set; } = new SaleDto();
    }
}
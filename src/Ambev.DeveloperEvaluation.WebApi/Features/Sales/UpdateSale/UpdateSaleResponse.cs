using Ambev.DeveloperEvaluation.Common.DTO;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// API response model for the UpdateSale operation.
    /// </summary>
    public class UpdateSaleResponse
    {
        /// <summary>
        /// Gets or sets the updated sale details.
        /// </summary>
        public SaleDto Sale { get; set; } = new SaleDto();
    }
}

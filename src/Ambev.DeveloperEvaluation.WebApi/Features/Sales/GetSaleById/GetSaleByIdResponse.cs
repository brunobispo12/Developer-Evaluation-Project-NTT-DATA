using Ambev.DeveloperEvaluation.Common.DTO;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    /// <summary>
    /// API response model for the GetSaleById operation.
    /// </summary>
    public class GetSaleByIdResponse
    {
        /// <summary>
        /// Gets or sets the sale details.
        /// </summary>
        public SaleDto Sale { get; set; } = new SaleDto();
    }
}

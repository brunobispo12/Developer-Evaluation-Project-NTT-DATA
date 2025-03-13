using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The Sale DTO
    /// </summary>
    
    public required SaleDto Sale { get; set; }
}

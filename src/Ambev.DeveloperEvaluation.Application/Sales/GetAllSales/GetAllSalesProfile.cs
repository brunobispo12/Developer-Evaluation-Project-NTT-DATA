using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

/// <summary>
/// AutoMapper profile for mapping the Sale entity to its corresponding DTO in GetAllSalesResult.
/// </summary>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Configures the mapping between Sale (domain entity) and SaleDto (data transfer object).
    /// </summary>
    public GetAllSalesProfile()
    {
        CreateMap<Sale, SaleDto>();
    }
}
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales
{
    /// <summary>
    /// AutoMapper profile for mapping between API and Application models for the GetAllSales feature.
    /// </summary>
    public class GetAllSalesProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the GetAllSales feature.
        /// </summary>
        public GetAllSalesProfile()
        {
            CreateMap<GetAllSalesRequest, GetAllSalesCommand>();
            CreateMap<GetAllSalesResult, GetAllSalesResponse>();
        }
    }
}

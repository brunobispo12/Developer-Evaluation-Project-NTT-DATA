using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    /// <summary>
    /// AutoMapper profile for mapping between API and Application models for the GetSaleById feature.
    /// </summary>
    public class GetSaleByIdProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the GetSaleById feature.
        /// </summary>
        public GetSaleByIdProfile()
        {
            CreateMap<GetSaleByIdRequest, GetSaleByIdCommand>();
            CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
        }
    }
}

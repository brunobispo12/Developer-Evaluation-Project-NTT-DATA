using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// AutoMapper profile for mapping between API UpdateSale models and application update sale commands/results.
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the UpdateSale feature.
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<SaleItemRequest, SaleItemDto>();
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());
            CreateMap<Sale, SaleDto>();
            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src));
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        }
    }
}

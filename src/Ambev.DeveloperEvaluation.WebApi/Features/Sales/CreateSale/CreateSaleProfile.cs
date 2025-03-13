using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between Application and API CreateSale models.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the CreateSale feature.
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<SaleItemRequest, SaleItemDto>();
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());
            CreateMap<Sale, SaleDto>();
            CreateMap<Sale, CreateSaleResult>()
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src));
            CreateMap<CreateSaleResult, CreateSaleResponse>();
        }
    }
}

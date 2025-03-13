using AutoMapper;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and CreateSaleResponse.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale operation.
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    foreach (var dto in src.Items)
                    {
                        dest.AddItem(dto.Product, dto.Quantity, dto.UnitPrice);
                    }

                    if (src.IsCancelled)
                    {
                        dest.CancelSale();
                    }
                });
            CreateMap<Sale, CreateSaleResult>()
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src));
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleItem, SaleItemDto>();
        }
    }
}

using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between <see cref="UpdateSaleCommand"/> and <see cref="Sale"/>.
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the update sale operation.
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Sale>();
            CreateMap<Sale, UpdateSaleResult>();


            CreateMap<UpdateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    var saleItems = src.Items.Select(dto =>
                        new SaleItem(
                            dest,
                            dto.Product,
                            dto.Quantity,
                            dto.UnitPrice,
                            dto.Discount
                        )
                    ).ToList();
                    dest.AddItems(saleItems);
                    if (src.IsCancelled)
                    {
                        dest.CancelSale();
                    }
                });
            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src));
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleItem, SaleItemDto>();
        }
    }
}

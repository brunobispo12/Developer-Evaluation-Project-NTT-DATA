using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    public class DeleteSaleProfile : Profile
    {
        public DeleteSaleProfile()
        {
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
            CreateMap<Guid, DeleteSaleCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
            CreateMap<DeleteSaleResult, DeleteSaleResponse>();
        }
    }
}

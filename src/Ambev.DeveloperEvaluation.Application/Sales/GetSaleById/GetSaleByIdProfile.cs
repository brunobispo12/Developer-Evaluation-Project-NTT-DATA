using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// AutoMapper profile for mapping between <see cref="Sale"/> and <see cref="GetSaleByIdResult"/>.
    /// </summary>
    public class GetSaleByIdProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for the GetSaleById operation.
        /// </summary>
        public GetSaleByIdProfile()
        {
            CreateMap<Sale, GetSaleByIdResult>();
        }
    }
}

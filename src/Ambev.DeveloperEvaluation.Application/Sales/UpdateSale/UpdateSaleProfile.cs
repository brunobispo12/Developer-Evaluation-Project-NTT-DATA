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
        }
    }
}

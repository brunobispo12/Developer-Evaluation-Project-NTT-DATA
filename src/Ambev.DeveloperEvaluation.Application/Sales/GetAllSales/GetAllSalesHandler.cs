using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Common.Helpers;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    /// <summary>
    /// Handler for processing GetAllSalesCommand requests.
    /// </summary>
    public class GetAllSalesHandler : IRequestHandler<GetAllSalesCommand, GetAllSalesResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetAllSalesHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetAllSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetAllSalesCommand request.
        /// </summary>
        /// <param name="command">The command with pagination and ordering parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A GetAllSalesResult containing the paginated list of SaleDto and metadata.</returns>
        public async Task<GetAllSalesResult> Handle(GetAllSalesCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetAllSalesCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            PaginatedList<Domain.Entities.Sale>? paginatedSales = await _saleRepository.GetAllAsync(
                command.PageNumber,
                command.PageSize,
                command.Order,
                cancellationToken
            );

            if (paginatedSales == null)
            {
                throw new System.NullReferenceException("The repository returned null for paginated sales.");
            }

            List<SaleDto> saleDtos = _mapper.Map<List<SaleDto>>(paginatedSales);

            return new GetAllSalesResult
            {
                Sales = saleDtos,
                PageNumber = command.PageNumber,
                PageSize = command.PageSize,
                TotalCount = paginatedSales.TotalCount,
                TotalPages = paginatedSales.TotalPages
            };
        }
    }
}

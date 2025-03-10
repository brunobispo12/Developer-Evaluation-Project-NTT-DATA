using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Handler for processing <see cref="GetSaleByIdCommand"/> requests.
    /// </summary>
    public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdCommand, GetSaleByIdResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="GetSaleByIdHandler"/>.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetSaleByIdHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the <see cref="GetSaleByIdCommand"/> request.
        /// </summary>
        /// <param name="command">The command containing the sale ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="GetSaleByIdResult"/> containing the sale details if found.</returns>
        /// <exception cref="ValidationException">Thrown when the command fails validation.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if no sale is found with the given ID.</exception>
        public async Task<GetSaleByIdResult> Handle(GetSaleByIdCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetSaleByIdCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {command.Id} was not found.");
            }

            var result = _mapper.Map<GetSaleByIdResult>(sale);
            return result;
        }
    }
}

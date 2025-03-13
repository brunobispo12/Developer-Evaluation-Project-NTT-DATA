using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ISaleNumberGenerator _saleNumberGenerator;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="saleNumberGenerator">The service used to generate the sale number.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ISaleNumberGenerator saleNumberGenerator)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _saleNumberGenerator = saleNumberGenerator;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale details as a full SaleDto.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            command.SaleNumber = await _saleNumberGenerator.GenerateSaleNumberAsync(command.SaleDate, cancellationToken);

            var sale = _mapper.Map<Sale>(command);
            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
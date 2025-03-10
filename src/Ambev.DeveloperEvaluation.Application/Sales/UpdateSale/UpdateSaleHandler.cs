using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing <see cref="UpdateSaleCommand"/> requests.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateSaleHandler"/>.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the <see cref="UpdateSaleCommand"/> request.
        /// </summary>
        /// <param name="command">The UpdateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated sale details.</returns>
        /// <exception cref="ValidationException">Thrown when the command fails validation.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the sale does not exist or if the sale number conflicts.</exception>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingSale == null)
            {
                throw new InvalidOperationException(
                    $"Sale with Id {command.Id} does not exist."
                );
            }

            var saleWithSameNumber = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
            if (saleWithSameNumber != null && saleWithSameNumber.Id != existingSale.Id)
            {
                throw new InvalidOperationException(
                    $"Sale with number {command.SaleNumber} already exists."
                );
            }

            _mapper.Map(command, existingSale);

            var updatedSale = await _saleRepository.UpdateAsync(existingSale, cancellationToken);

            var result = _mapper.Map<UpdateSaleResult>(updatedSale);

            return result;
        }
    }
}
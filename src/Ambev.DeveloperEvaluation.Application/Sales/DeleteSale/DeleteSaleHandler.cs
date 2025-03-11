using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handler for processing <see cref="DeleteSaleCommand"/> requests.
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteSaleHandler"/>.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        public DeleteSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Handles the DeleteSaleCommand request.
        /// </summary>
        /// <param name="command">The command containing the sale ID to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="DeleteSaleResult"/> indicating whether the deletion succeeded.</returns>
        /// <exception cref="ValidationException">Thrown when the command fails validation.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if no sale is found with the given ID.</exception>
        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            bool deleted = await _saleRepository.DeleteAsync(command.Id, cancellationToken);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Sale with ID {command.Id} was not found.");
            }

            return new DeleteSaleResult { Success = true };
        }
    }
}

using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command for deleting an existing sale by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This command captures the sale ID needed to perform the deletion.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request
    /// that returns a <see cref="DeleteSaleResult"/>.
    /// </remarks>
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Validates the command using the <see cref="DeleteSaleCommandValidator"/> rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <c>IsValid</c>: True if all validation rules pass.
        /// - <c>Errors</c>: A collection of validation errors if any rules fail.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new DeleteSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

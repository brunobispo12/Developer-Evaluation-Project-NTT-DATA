using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Command for retrieving a single sale by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This command captures the necessary parameter for retrieving
    /// a sale (the sale's unique Guid). 
    /// 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate
    /// the request that returns a <see cref="GetSaleByIdResult"/>.
    /// </remarks>
    public class GetSaleByIdCommand : IRequest<GetSaleByIdResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Validates the command using the <see cref="GetSaleByIdCommandValidator"/> rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <c>IsValid</c>: True if all validation rules pass.
        /// - <c>Errors</c>: A collection of validation errors if any rules fail.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new GetSaleByIdCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

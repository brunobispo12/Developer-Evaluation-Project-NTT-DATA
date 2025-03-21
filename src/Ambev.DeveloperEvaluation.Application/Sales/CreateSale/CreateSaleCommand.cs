﻿using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command captures the necessary data for creating a sale, including:
    /// - Sale number
    /// - Sale date
    /// - Customer identification
    /// - Branch where the sale was made
    /// - A list of sale items (each containing product, quantity, and unit price)
    /// 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request that returns a <see cref="CreateSaleResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CreateSaleCommandValidator"/>, which extends 
    /// <see cref="FluentValidation.AbstractValidator{T}"/> to ensure that all fields are correctly populated and follow the required rules.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sale date.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer identification.
        /// </summary>
        public Guid Customer { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        public Guid Branch { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the sale is canceled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the list of sale items.
        /// </summary>
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();

        /// <summary>
        /// Validates the command using the CreateSaleCommandValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <c>IsValid</c>: True if all validation rules pass.
        /// - <c>Errors</c>: A collection of validation errors if any rules fail.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
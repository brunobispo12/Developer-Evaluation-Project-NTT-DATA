using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the product identification (from an external domain).
        /// </summary>
        public Guid Product { get; private set; }

        /// <summary>
        /// Gets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the applied discount (percentage value, e.g., 0.10 for 10%).
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets the total price of the item, taking the discount into account.
        /// </summary>
        public decimal TotalPrice => Quantity * UnitPrice * (1 - Discount);

        /// <summary>
        /// Gets the ID of the associated sale.
        /// </summary>
        public Guid SaleId { get; private set; }

        /// <summary>
        /// Gets the sale to which this item belongs.
        /// </summary>
        public Sale Sale { get; private set; }

        /// <summary>
        /// Private constructor for EF Core materialization.
        /// Initializes default values for properties that cannot be null.
        /// </summary>
        private SaleItem()
        {
            Product = Guid.Empty;
            Sale = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class.
        /// </summary>
        /// <param name="sale">The sale entity to which this item belongs.</param>
        /// <param name="product">Product identification (external ID).</param>
        /// <param name="quantity">Quantity of the product.</param>
        /// <param name="unitPrice">Unit price of the product.</param>
        /// <param name="discount">Discount applied (e.g., 0.10 for 10%).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="sale"/> is null.</exception>
        public SaleItem(Sale sale, Guid product, int quantity, decimal unitPrice, decimal discount)
        {
            Sale = sale ?? throw new ArgumentNullException(nameof(sale));
            SaleId = sale.Id;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
        }

        /// <summary>
        /// Performs validation of the <see cref="SaleItem"/> entity using the <see cref="SaleItemValidator"/> rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <c>IsValid</c>: Indicates whether all validation rules passed.
        /// - <c>Errors</c>: A collection of validation errors if any rules failed.
        /// </returns>
        /// <remarks>
        /// The validation checks include:
        /// <list type="bullet">
        ///     <item>
        ///         <description>Product identifier is not empty.</description>
        ///     </item>
        ///     <item>
        ///         <description>Quantity is greater than 0 and no more than 20.</description>
        ///     </item>
        ///     <item>
        ///         <description>Unit price is greater than 0.</description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///             Discount rules:
        ///             <list type="bullet">
        ///                 <item><description>No discount for quantities below 4.</description></item>
        ///                 <item><description>10% discount for quantities between 4 and 9.</description></item>
        ///                 <item><description>20% discount for quantities between 10 and 20.</description></item>
        ///             </list>
        ///         </description>
        ///     </item>
        /// </list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

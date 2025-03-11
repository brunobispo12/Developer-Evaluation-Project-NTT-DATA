namespace Ambev.DeveloperEvaluation.Application.Sales
{
    /// <summary>
    /// Represents a sale item Data Transfer Object (DTO).
    /// </summary>
    public class SaleItemDto
    {
        /// <summary>
        /// Gets or sets the product identification (external ID).
        /// </summary>
        public Guid Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}

namespace Ambev.DeveloperEvaluation.Common.DTO
{
    /// <summary>
    /// Data Transfer Object for a SaleItem.
    /// </summary>
    public class SaleItemDto
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public Guid Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the applied discount (e.g., 0.10 for 10%).
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total price of the sale item.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}

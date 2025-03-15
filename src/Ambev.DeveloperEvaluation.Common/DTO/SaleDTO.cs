namespace Ambev.DeveloperEvaluation.Common.DTO
{
    /// <summary>
    /// Data Transfer Object for a Sale.
    /// </summary>
    public class SaleDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the sale was made.
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
    }
}
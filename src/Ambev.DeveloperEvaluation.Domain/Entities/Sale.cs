using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sales record in the system.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Private list to store sale items.
        /// </summary>
        private readonly List<SaleItem> _items = [];

        /// <summary>
        /// Gets a read-only collection of sale items.
        /// </summary>
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Gets the sale number.
        /// </summary>
        public string SaleNumber { get; private set; }

        /// <summary>
        /// Gets the date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; private set; }

        /// <summary>
        /// Gets the customer identification (external identity from another domain).
        /// </summary>
        public Guid Customer { get; private set; }

        /// <summary>
        /// Gets the branch where the sale was made (external identity from another domain).
        /// </summary>
        public Guid Branch { get; private set; }

        /// <summary>
        /// Indicates if the sale has been canceled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount => _items.Sum(item => item.TotalPrice);

        /// <summary>
        /// Parameterless constructor for EF Core (if needed).
        /// </summary>
        private Sale()
        {
            _items = [];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sale"/> class.
        /// </summary>
        /// <param name="saleNumber">Sale number.</param>
        /// <param name="saleDate">Date of sale.</param>
        /// <param name="customer">Customer identification.</param>
        /// <param name="branch">Branch where the sale was made.</param>
        public Sale(string saleNumber, DateTime saleDate, Guid customer, Guid branch)
            : this()
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            Customer = customer;
            Branch = branch;
            IsCancelled = false;
        }

        /// <summary>
        /// Adds multiple items to the sale in one go.
        /// </summary>
        /// <param name="items">The items to add.</param>
        public void AddItems(IEnumerable<SaleItem> items)
        {
            _items.AddRange(items);
        }

        /// <summary>
        /// Adds an item to the sale, applying business discount rules.
        /// </summary>
        /// <param name="product">Product identification.</param>
        /// <param name="quantity">Quantity of the product.</param>
        /// <param name="unitPrice">Unit price of the product.</param>
        public void AddItem(Guid product, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            decimal discount = quantity >= 10 ? 0.20m : quantity >= 4 ? 0.10m : 0m;

            var saleItem = new SaleItem(this, product, quantity, unitPrice, discount);
            _items.Add(saleItem);
        }

        /// <summary>
        /// Cancels the sale.
        /// </summary>
        public void CancelSale()
        {
            IsCancelled = true;
        }

        /// <summary>
        /// Performs validation of the sale entity using the <see cref="SaleValidator"/> rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - <c>IsValid</c>: Indicates whether all validation rules passed.
        /// - <c>Errors</c>: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    /// <summary>
    /// Provides functionality to generate a sale number following a specific formatted pattern.
    /// </summary>
    public class SaleNumberGenerator : ISaleNumberGenerator
    {
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleNumberGenerator"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository used to access sales data.</param>
        public SaleNumberGenerator(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Generates the next sale number based on the provided sale date.
        /// </summary>
        /// <param name="saleDate">The date of the sale.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A formatted sale number in the pattern "DS-YYYYMMDD-XXXXXX". For example, "DS-20250312-000001".
        /// </returns>
        public async Task<string> GenerateSaleNumberAsync(DateTime saleDate, CancellationToken cancellationToken = default)
        {
            string datePart = saleDate.ToString("yyyyMMdd");

            var lastSale = await _saleRepository.GetLastSaleForDateAsync(saleDate, cancellationToken);

            int nextSequence = 1;
            if (lastSale != null)
            {
                var parts = lastSale.SaleNumber.Split('-');
                if (parts.Length == 3 && int.TryParse(parts[2], out int lastSequence))
                {
                    nextSequence = lastSequence + 1;
                }
            }

            return $"DS-{datePart}-{nextSequence:D6}";
        }
    }
}

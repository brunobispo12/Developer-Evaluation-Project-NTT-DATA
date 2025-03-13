namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleNumberGenerator
    {
        /// <summary>
        /// Generate the next SaleNumber
        /// </summary>
        /// <param name="saleDate">Sale date.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>SaleNumber in format "AMV-YYYYMMDD-000001".</returns>
        Task<string> GenerateSaleNumberAsync(DateTime saleDate, CancellationToken cancellationToken = default);
    }
}

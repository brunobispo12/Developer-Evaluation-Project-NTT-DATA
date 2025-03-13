using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Creates a new sale in the repository.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale.</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing sale in the repository.
        /// </summary>
        /// <param name="sale">The sale with updated data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated sale.</returns>
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its sale number.
        /// </summary>
        /// <param name="saleNumber">The sale number to search for.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all the sales with pagination and ordering support.
        /// </summary>
        /// <param name="pageNumber">The page number (starting at 1).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="order">
        /// Optional ordering string (e.g., "SaleNumber desc, SaleDate asc"). 
        /// If not provided, a default ordering is applied.
        /// </param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paginated list of sales.</returns>
        Task<PaginatedList<Sale>> GetAllAsync(int pageNumber, int pageSize, string? order, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes a sale from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the sale was deleted, false if not found.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the last sale created for a specific date,
        /// based on the sale number pattern (e.g., "DS-YYYYMMDD-XXXXXX").
        /// </summary>
        /// <param name="saleDate">The date of the sale to filter by.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The last matching sale, or <c>null</c> if none is found.</returns>
        Task<Sale?> GetLastSaleForDateAsync(DateTime saleDate, CancellationToken cancellationToken = default);
    }
}
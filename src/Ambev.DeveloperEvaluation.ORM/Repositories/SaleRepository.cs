using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the SaleRepository class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale in the database.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale.</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Updates an existing sale in the database.
        /// </summary>
        /// <param name="sale">The sale with updated data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated sale.</returns>
        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a sale by its sale number.
        /// </summary>
        /// <param name="saleNumber">The sale number to search for.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
        }

        /// <summary>
        /// Deletes a sale from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the sale was deleted, false if not found.</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Retrieves all the sales.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all sales.</returns>
        public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves all the sales with pagination and ordering support.
        /// </summary>
        /// <param name="pageNumber">The page number (starting at 1).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="order">
        /// Optional ordering string (e.g., "SaleNumber desc, SaleDate asc").
        /// If not provided, a default ordering (by SaleDate descending) is applied.
        /// </param>
        public async Task<PaginatedList<Sale>> GetAllAsync(int pageNumber, int pageSize, string? order, CancellationToken cancellationToken = default)
        {
            IQueryable<Sale> query = _context.Sales.Include(s => s.Items).AsQueryable();

            if (!string.IsNullOrWhiteSpace(order))
            {
                query = query.OrderBy(order);
            }
            else
            {
                query = query.OrderByDescending(s => s.SaleDate);
            }

            return await PaginatedList<Sale>.CreateAsync(query, pageNumber, pageSize);
        }

        /// <summary>
        /// Retrieves the last sale created for a specific date, 
        /// based on the sale number pattern (e.g., "DS-YYYYMMDD-XXXXXX").
        /// </summary>
        /// <param name="saleDate">The date of the sale to filter by.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The last matching <see cref="Sale"/> found for the given date, or <c>null</c> if none is found.</returns>
        public async Task<Sale?> GetLastSaleForDateAsync(DateTime saleDate, CancellationToken cancellationToken = default)
        {
            string datePart = saleDate.ToString("yyyyMMdd");
            return await _context.Sales
                .Where(s => s.SaleNumber.StartsWith($"DS-{datePart}-"))
                .OrderByDescending(s => s.SaleNumber)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
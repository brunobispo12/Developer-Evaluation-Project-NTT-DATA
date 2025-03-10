using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data for SaleItem entity using the Bogus library.
    /// </summary>
    public static class SaleItemTestData
    {
        private static readonly Faker _faker = new Faker("pt_BR");

        /// <summary>
        /// Generates a valid SaleItem belonging to a given Sale.
        /// </summary>
        public static SaleItem GenerateValidSaleItem(Sale sale)
        {
            var quantity = _faker.Random.Int(1, 20);     // 1..20

            // Calculate discount for isolated testing purposes only (even though in practice
            // Sale's AddItem would do this).
            decimal discount = quantity >= 10 ? 0.20m : quantity >= 4 ? 0.10m : 0m;

            return new SaleItem(
                sale: sale,
                product: Guid.NewGuid(),
                quantity: quantity,
                unitPrice: _faker.Random.Decimal(1, 999),
                discount: discount
            );
        }

        /// <summary>
        /// Generates an invalid SaleItem for negative testing.
        /// For example, zero quantity, negative price, or empty Product, etc.
        /// </summary>
        public static SaleItem GenerateInvalidSaleItem(Sale sale)
        {
            return new SaleItem(
                sale: sale,
                product: Guid.Empty,
                quantity: 0,
                unitPrice: -10,
                discount: 0.5m
            );
        }
    }
}

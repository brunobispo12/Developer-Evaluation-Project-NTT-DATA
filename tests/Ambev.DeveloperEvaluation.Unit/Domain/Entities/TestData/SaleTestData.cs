using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data for Sale entity using the Bogus library.
    /// </summary>
    public static class SaleTestData
    {
        private static readonly Faker _faker = new Faker("pt_BR");

        /// <summary>
        /// Generates a valid Sale with random data.
        /// </summary>
        public static Sale GenerateValidSale()
        {
            return new Sale(
                saleNumber: _faker.Random.AlphaNumeric(8),
                saleDate: _faker.Date.Past(1),
                customer: _faker.Random.Guid(),
                branch: _faker.Random.Guid()
            );
        }

        /// <summary>
        /// Generates an invalid Sale with missing/empty fields for negative tests.
        /// For example, empty SaleNumber or invalid date, etc.
        /// </summary>
        public static Sale GenerateInvalidSale()
        {
            return new Sale(
                saleNumber: string.Empty,
                saleDate: DateTime.MinValue,
                customer: Guid.NewGuid(),
                branch: Guid.NewGuid()
            );
        }
    }
}

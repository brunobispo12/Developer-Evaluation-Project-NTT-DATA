using Bogus;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.DTO;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides helper methods for generating test data for the UpdateSaleHandler.
    /// This class centralizes all test data generation to ensure consistency across test cases.
    /// </summary>
    public static class UpdateSaleHandlerTestData
    {
        private static readonly Faker<SaleItemDto> itemFaker = new Faker<SaleItemDto>()
            .RuleFor(i => i.Product, f => Guid.NewGuid())
            .RuleFor(i => i.Quantity, f => f.Random.Int(min: 1, max: 20))
            .RuleFor(i => i.UnitPrice, f => Math.Round(f.Finance.Amount(1, 100), 2));

        private static readonly Faker<UpdateSaleCommand> commandFaker = new Faker<UpdateSaleCommand>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.SaleDate, f => f.Date.Past(1))
            .RuleFor(x => x.Customer, f => Guid.NewGuid())
            .RuleFor(x => x.Branch, f => Guid.NewGuid())
            .RuleFor(x => x.Items, f => itemFaker.Generate(f.Random.Int(min: 1, max: 5)));

        /// <summary>
        /// Generates a valid UpdateSaleCommand with random but valid data.
        /// </summary>
        /// <returns>An UpdateSaleCommand containing valid data.</returns>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            return commandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid UpdateSaleCommand for testing validation failures.
        /// For example, an empty SaleNumber or a future SaleDate or empty items.
        /// </summary>
        /// <returns>An UpdateSaleCommand that should fail validation.</returns>
        public static UpdateSaleCommand GenerateInvalidCommand()
        {
            return new UpdateSaleCommand
            {
                Id = Guid.Empty,
                SaleDate = DateTime.Now.AddDays(7),
                Customer = Guid.Empty,
                Branch = Guid.Empty,
                Items = new List<SaleItemDto>()
            };
        }
    }
}

using Bogus;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.DTO;


namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides helper methods for generating test data for the CreateSaleHandler.
    /// This class centralizes all test data generation to ensure consistency across test cases.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        private static readonly Faker<SaleItemDto> itemFaker = new Faker<SaleItemDto>()
            .RuleFor(i => i.Product, f => Guid.NewGuid())
            .RuleFor(i => i.Quantity, f => f.Random.Int(min: 1, max: 20))
            .RuleFor(i => i.UnitPrice, f => Math.Round(f.Finance.Amount(1, 100), 2));

        private static readonly Faker<CreateSaleCommand> commandFaker = new Faker<CreateSaleCommand>()
            .RuleFor(x => x.SaleNumber, f => f.Random.AlphaNumeric(8))
            .RuleFor(x => x.SaleDate, f => f.Date.Past(1))
            .RuleFor(x => x.Customer, f => f.Person.FullName)
            .RuleFor(x => x.Branch, f => f.Company.CompanyName())
            .RuleFor(x => x.Items, f => itemFaker.Generate(f.Random.Int(min: 1, max: 5)));

        /// <summary>
        /// Generates a valid CreateSaleCommand with random but valid data.
        /// </summary>
        /// <returns>A CreateSaleCommand containing valid data.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            return commandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid CreateSaleCommand for testing validation failures.
        /// For example, an empty SaleNumber or a future SaleDate.
        /// </summary>
        /// <returns>A CreateSaleCommand that should fail validation.</returns>
        public static CreateSaleCommand GenerateInvalidCommand()
        {
            return new CreateSaleCommand
            {
                SaleNumber = string.Empty,
                SaleDate = DateTime.Now.AddDays(7),
                Customer = string.Empty,
                Branch = string.Empty,
                Items = new List<SaleItemDto>()
            };
        }
    }
}
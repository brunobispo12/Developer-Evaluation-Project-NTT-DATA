using Bogus;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides helper methods for generating test data for the GetAllSalesHandler.
    /// </summary>
    public static class GetAllSalesHandlerTestData
    {
        private static readonly Faker<GetAllSalesCommand> commandFaker = new Faker<GetAllSalesCommand>()
            .RuleFor(x => x.PageNumber, f => f.Random.Int(min: 1, max: 5))
            .RuleFor(x => x.PageSize, f => f.Random.Int(min: 1, max: 20))
            .RuleFor(x => x.Order, f => f.PickRandom<string?>(null, "SaleDate desc", "SaleNumber asc"));

        /// <summary>
        /// Generates a valid GetAllSalesCommand with random but valid data.
        /// </summary>
        /// <returns>A GetAllSalesCommand containing valid data.</returns>
        public static GetAllSalesCommand GenerateValidCommand()
        {
            return commandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid GetAllSalesCommand for testing validation failures.
        /// For example, PageNumber set to zero or negative.
        /// </summary>
        /// <returns>A GetAllSalesCommand that should fail validation.</returns>
        public static GetAllSalesCommand GenerateInvalidCommand()
        {
            var command = commandFaker.Generate();
            command.PageNumber = 0;
            return command;
        }
    }
}
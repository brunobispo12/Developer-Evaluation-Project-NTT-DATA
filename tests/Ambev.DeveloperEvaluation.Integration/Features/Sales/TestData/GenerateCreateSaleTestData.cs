using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales.TestData
{
    class GenerateCreateSaleTestData
    {
        public static CreateSaleRequest GenerateSaleRequest()
        {
            var saleItemFaker = new Faker<SaleItemRequest>()
                .RuleFor(item => item.Product, f => Guid.NewGuid())
                .RuleFor(item => item.Quantity, f => f.Random.Int(1, 20))
                .RuleFor(item => item.UnitPrice, f => f.Random.Decimal(10.0m, 100.0m));

            var saleFaker = new Faker<CreateSaleRequest>()
                .RuleFor(sale => sale.SaleDate, f => DateTime.UtcNow.AddDays(-1))
                .RuleFor(sale => sale.Customer, f => Guid.NewGuid())
                .RuleFor(sale => sale.Branch, f => Guid.NewGuid())
                .RuleFor(sale => sale.IsCancelled, f => false)
                .RuleFor(sale => sale.Items, f => saleItemFaker.Generate(f.Random.Int(1, 3)));

            return saleFaker.Generate();
        }
    }
}

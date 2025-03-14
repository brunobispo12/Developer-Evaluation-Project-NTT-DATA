using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services
{
    public class SaleNumberGeneratorTests
    {
        [Fact(DisplayName = "GenerateSaleNumberAsync returns first sequence when no sale exists for the date")]
        public async Task GenerateSaleNumberAsync_NoExistingSale_ReturnsFirstSequence()
        {
            // Arrange
            var saleDate = new DateTime(2025, 3, 12);
            var saleRepository = Substitute.For<ISaleRepository>();

            saleRepository.GetLastSaleForDateAsync(saleDate, Arg.Any<CancellationToken>())
                          .Returns(Task.FromResult<Sale?>(null));

            var generator = new SaleNumberGenerator(saleRepository);

            // Act
            var saleNumber = await generator.GenerateSaleNumberAsync(saleDate, CancellationToken.None);

            // Assert
            saleNumber.Should().Be("DS-20250312-000001");
        }

        [Fact(DisplayName = "GenerateSaleNumberAsync returns next sequence when an existing sale exists for the date")]
        public async Task GenerateSaleNumberAsync_ExistingSale_ReturnsNextSequence()
        {
            // Arrange
            var saleDate = new DateTime(2025, 3, 12);
            var saleRepository = Substitute.For<ISaleRepository>();

            var existingSale = new Sale("DS-20250312-000005", saleDate, "TestCustomer", "TestBranch")
            {
                Id = Guid.NewGuid()
            };

            saleRepository.GetLastSaleForDateAsync(saleDate, Arg.Any<CancellationToken>())
                          .Returns(Task.FromResult<Sale?>(existingSale));

            var generator = new SaleNumberGenerator(saleRepository);

            // Act
            var saleNumber = await generator.GenerateSaleNumberAsync(saleDate, CancellationToken.None);

            // Assert
            saleNumber.Should().Be("DS-20250312-000006");
        }
    }
}

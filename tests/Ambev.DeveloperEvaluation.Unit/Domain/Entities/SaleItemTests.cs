using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the SaleItem entity class.
    /// Tests cover validation scenarios and calculations such as total price.
    /// </summary>
    public class SaleItemTests
    {
        /// <summary>
        /// Tests that validation passes for a valid SaleItem.
        /// </summary>
        [Fact(DisplayName = "Validation should pass for valid sale item data")]
        public void Given_ValidSaleItemData_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = SaleItemTestData.GenerateValidSaleItem(sale);

            // Act
            var result = saleItem.Validate();

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Tests that validation fails for an invalid SaleItem.
        /// </summary>
        [Fact(DisplayName = "Validation should fail for invalid sale item data")]
        public void Given_InvalidSaleItemData_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem(sale);

            // Act
            var result = saleItem.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Tests that TotalPrice is calculated correctly, including discount.
        /// For example, Q=10, Price=10, Discount=20% => total = (10 * 10 * 0.8) = 80.
        /// </summary>
        [Fact(DisplayName = "TotalPrice should reflect correct discount calculation")]
        public void Given_SaleItemWithDiscount_When_CheckingTotalPrice_Then_ShouldBeCorrect()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = new SaleItem(sale, product: System.Guid.NewGuid(), quantity: 10, unitPrice: 10m, discount: 0.20m);

            // Act
            var totalPrice = saleItem.TotalPrice;

            // Assert
            Assert.Equal(80m, totalPrice);
        }
    }
}

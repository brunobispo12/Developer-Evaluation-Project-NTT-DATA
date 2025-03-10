using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the Sale entity class.
    /// Tests cover validation scenarios, canceling a sale,
    /// and adding items with discount rules.
    /// </summary>
    public class SaleTests
    {
        /// <summary>
        /// Tests that validation passes when all Sale properties are valid.
        /// </summary>
        [Fact(DisplayName = "Validation should pass for valid sale data")]
        public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Tests that validation fails when Sale properties are invalid.
        /// </summary>
        [Fact(DisplayName = "Validation should fail for invalid sale data")]
        public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var sale = SaleTestData.GenerateInvalidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Tests that when AddItem is called with a quantity of 21,
        /// it throws an InvalidOperationException.
        /// </summary>
        [Fact(DisplayName = "AddItem should throw when quantity > 20")]
        public void Given_Sale_When_AddingItemWithMoreThan20_Then_ShouldThrow()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            Action act = () => sale.AddItem(Guid.NewGuid(), 21, 10m);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        /// <summary>
        /// Tests that when CancelSale is called,
        /// the sale's IsCancelled property becomes true.
        /// </summary>
        [Fact(DisplayName = "CancelSale should mark sale as cancelled")]
        public void Given_Sale_When_CancelSaleCalled_Then_ShouldBeCancelled()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.CancelSale();

            // Assert
            Assert.True(sale.IsCancelled);
        }

        /// <summary>
        /// Tests that AddItem applies the correct 10% discount for quantity >= 4 and < 10.
        /// </summary>
        [Fact(DisplayName = "AddItem should apply 10% discount for quantity between 4 and 9")]
        public void Given_Sale_When_QuantityBetween4and9_Then_ShouldApply10PercentDiscount()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.AddItem(Guid.NewGuid(), 5, 100m); // 5 => 10% discount

            // Assert
            var item = Assert.Single(sale.Items);
            Assert.Equal(0.10m, item.Discount);
        }

        /// <summary>
        /// Tests that AddItem applies the correct 20% discount for quantity >= 10.
        /// </summary>
        [Fact(DisplayName = "AddItem should apply 20% discount for quantity >= 10")]
        public void Given_Sale_When_QuantityAtLeast10_Then_ShouldApply20PercentDiscount()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.AddItem(Guid.NewGuid(), 10, 50m);

            // Assert
            var item = Assert.Single(sale.Items);
            Assert.Equal(0.20m, item.Discount);
        }

        /// <summary>
        /// Tests that the TotalAmount property sums the TotalPrice of all items added.
        /// </summary>
        [Fact(DisplayName = "TotalAmount should sum all SaleItem total prices")]
        public void Given_SaleWithMultipleItems_When_CheckingTotalAmount_Then_ShouldReflectSum()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.AddItem(Guid.NewGuid(), 5, 20m);
            sale.AddItem(Guid.NewGuid(), 2, 50m);

            // Act
            var total = sale.TotalAmount;

            // Assert
            Assert.Equal(190m, total);
        }
    }
}
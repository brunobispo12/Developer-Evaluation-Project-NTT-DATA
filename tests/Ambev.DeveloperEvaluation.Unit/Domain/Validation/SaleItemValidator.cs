using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    /// <summary>
    /// Contains unit tests for the SaleItemValidator class.
    /// Tests cover validation of all SaleItem properties including Product, Quantity,
    /// UnitPrice, Discount, and so forth.
    /// </summary>
    public class SaleItemValidatorTests
    {
        private readonly SaleItemValidator _validator;

        public SaleItemValidatorTests()
        {
            _validator = new SaleItemValidator();
        }

        /// <summary>
        /// Tests that validation passes when all SaleItem properties are valid.
        /// </summary>
        [Fact(DisplayName = "Valid sale item should pass all validation rules")]
        public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = SaleItemTestData.GenerateValidSaleItem(sale);

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests that validation fails for invalid SaleItem data
        /// (e.g., Product=Guid.Empty, Quantity=0, negative UnitPrice, discount out of valid range).
        /// </summary>
        [Fact(DisplayName = "Invalid sale item data should fail validation")]
        public void Given_InvalidSaleItem_When_Validated_Then_ShouldHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem(sale);

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Example test verifying a specific rule for Quantity (e.g., must be between 1 and 20).
        /// </summary>
        [Fact(DisplayName = "Quantity outside 1..20 should fail validation")]
        public void Given_QuantityExceedingRange_When_Validated_Then_ShouldHaveErrorForQuantity()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = SaleItemTestData.GenerateValidSaleItem(sale);
            saleItem.GetType().GetProperty("Quantity")!.SetValue(saleItem, 25);

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        /// <summary>
        /// Example test verifying that Product cannot be Guid.Empty.
        /// </summary>
        [Fact(DisplayName = "Product cannot be Guid.Empty")]
        public void Given_EmptyProduct_When_Validated_Then_ShouldHaveErrorForProduct()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = SaleItemTestData.GenerateValidSaleItem(sale);
            saleItem.GetType().GetProperty("Product")!.SetValue(saleItem, System.Guid.Empty);

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Product);
        }
    }
}

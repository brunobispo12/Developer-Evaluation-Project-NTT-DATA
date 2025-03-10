using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    /// <summary>
    /// Contains unit tests for the SaleValidator class.
    /// Tests cover validation of all Sale properties including SaleNumber, SaleDate,
    /// Customer, Branch, and other rules defined in SaleValidator.
    /// </summary>
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator;

        public SaleValidatorTests()
        {
            _validator = new SaleValidator();
        }

        /// <summary>
        /// Tests that validation passes when all Sale properties are valid.
        /// </summary>
        [Fact(DisplayName = "Valid sale should pass all validation rules")]
        public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.AddItem(Guid.NewGuid(), 5, 100m);

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests that validation fails for invalid Sale data
        /// (e.g., empty SaleNumber, DateTime.MinValue, empty Customer/Branch).
        /// </summary>
        [Fact(DisplayName = "Invalid sale data should fail validation")]
        public void Given_InvalidSale_When_Validated_Then_ShouldHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateInvalidSale();

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Example test verifying a specific rule for SaleNumber (e.g., not empty).
        /// Adjust the scenario and rule name based on your real SaleValidator rules.
        /// </summary>
        [Fact(DisplayName = "SaleNumber cannot be empty")]
        public void Given_EmptySaleNumber_When_Validated_Then_ShouldHaveErrorForSaleNumber()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.GetType().GetProperty("SaleNumber")!.SetValue(sale, string.Empty);

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
        }

        /// <summary>
        /// Example test verifying a specific rule for Customer (e.g., not empty).
        /// </summary>
        [Fact(DisplayName = "Customer cannot be empty")]
        public void Given_EmptyCustomer_When_Validated_Then_ShouldHaveErrorForCustomer()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.GetType().GetProperty("Customer")!.SetValue(sale, string.Empty);

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Customer);
        }
    }
}

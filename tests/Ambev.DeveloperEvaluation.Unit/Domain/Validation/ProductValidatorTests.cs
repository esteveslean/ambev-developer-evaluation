using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;


public class ProductValidatorTests
{
    private readonly ProductValidator _validator = new();

    [Fact(DisplayName = "Valid product should pass all validation rules")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Theory(DisplayName = "Invalid product title formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("la")] // Less than 3 characters
    public void Given_InvalidTitle_When_Validated_Then_ShouldHaveError(string title)
    {
        // Arrange
        var product = ProductTestData.GenerateValid();
        product.Title = title;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
    
    [Theory(DisplayName = "Invalid product category formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("la")] // Less than 3 characters
    public void Given_InvalidCategory_When_Validated_Then_ShouldHaveError(string category)
    {
        // Arrange
        var product = ProductTestData.GenerateValid();
        product.Category = category;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Category);
    }
}

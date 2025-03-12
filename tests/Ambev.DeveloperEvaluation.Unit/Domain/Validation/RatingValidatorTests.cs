using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class RatingValidatorTests
{
    private readonly RatingValidator _validator = new();
    
    [Fact(DisplayName = "Valid rating should pass all validation rules")]
    public void Given_ValidRating_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        var result = _validator.TestValidate(product.Rating);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Theory(DisplayName = "Invalid rate formats should fail validation")]
    [InlineData(-1)]
    [InlineData(5.5)]
    [InlineData(6)]
    public void Given_ValidRate_When_Validated_Then_ShouldNotHaveErrors(float rate)
    {
        // Arrange
        var product = ProductTestData.GenerateValid();
        product.Rating.Rate = rate;

        // Act
        var result = _validator.TestValidate(product.Rating);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Rate);
    }
    
    [Theory(DisplayName = "Invalid count formats should fail validation")]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Given_ValidCount_When_Validated_Then_ShouldNotHaveErrors(int count)
    {
        // Arrange
        var product = ProductTestData.GenerateValid();
        product.Rating.Count = count;

        // Act
        var result = _validator.TestValidate(product.Rating);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Count);
    }
}
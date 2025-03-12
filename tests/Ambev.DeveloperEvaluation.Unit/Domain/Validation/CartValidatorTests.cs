using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;


public class CartValidatorTests
{
    private readonly CartValidator _validator = new();

    [Fact(DisplayName = "Valid cart should pass all validation rules")]
    public void Given_ValidCart_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();

        // Act
        var result = _validator.TestValidate(cart);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact(DisplayName = "Invalid cart user formats should fail validation")]
    public void Given_InvalidCartUser_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var cart = new Cart(Guid.Empty);

        // Act
        var result = _validator.TestValidate(cart);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }
}

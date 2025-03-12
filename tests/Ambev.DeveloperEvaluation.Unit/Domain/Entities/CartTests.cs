using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class CartTests
{
    [Fact(DisplayName = "Validation should pass for valid cart")]
    public void Given_ValidCart_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();

        // Act
        var result = cart.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact(DisplayName = "Validation should fail for invalid cart")]
    public void Given_InvalidCart_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var userId = Guid.Empty;
        var cart = new Cart(userId);

        // Act
        var result = cart.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
    
    [Theory(DisplayName = "AddProduct_AmountLessOrEqualZero_ShouldThrowException")]
    [InlineData(-4)]
    [InlineData(0)]
    public void AddProduct_AmountLessOrEqualZero_ShouldThrowException(int amount)
    {
        var cart = CartTestData.GenerateValid(1);

        var action = () => cart.AddProduct(Guid.NewGuid(), amount);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Quantity must be greater than zero.");
    }
    
    [Theory(DisplayName = "AddProductItem_AmountLessOrEqualZero_ShouldThrowException")]
    [InlineData(-4)]
    [InlineData(0)]
    public void AddProductItem_AmountLessOrEqualZero_ShouldThrowException(int amount)
    {
        var cartItem = new CartItem { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Amount = 0 };

        var action = () => cartItem.UpdateAmount(amount);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Quantity must be greater than zero.");
    }
}
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class OrderItemValidatorTests
{
    private readonly OrderItemValidator _validator = new();
    
    [Fact(DisplayName = "Valid order items should pass all validation rules")]
    public void Given_ValidOrderItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var order = OrderTestData.GenerateValid();
        var orderItem = order.Items[0];

        // Act
        var result = _validator.TestValidate(orderItem);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact(DisplayName = "Invalid order item product formats should fail validation")]
    public void Given_InvalidOrderItemProduct_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var orderItem = new OrderItem();

        // Act
        var result = _validator.TestValidate(orderItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }
    
    [Theory(DisplayName = "Invalid order item price formats should fail validation")]
    [InlineData(0)] // less than 1
    [InlineData(-1)] // less than 1
    [InlineData(-10.00)] // less than 1
    public void Given_InvalidOrderItemPrice_When_Validated_Then_ShouldHaveError(decimal unitPrice)
    {
        // Arrange
        var order = OrderTestData.GenerateValid();
        var orderItem = order.Items[0];
        orderItem.UnitPrice = unitPrice;

        // Act
        var result = _validator.TestValidate(orderItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
    }
    
    [Theory(DisplayName = "Invalid order item amount formats should fail validation")]
    [InlineData(0)] // less than 1
    [InlineData(-1)] // less than 1
    [InlineData(-10)] // less than 1
    public void Given_InvalidOrderItemAmount_When_Validated_Then_ShouldHaveError(int amount)
    {
        // Arrange
        var order = OrderTestData.GenerateValid();
        var orderItem = order.Items[0];
        orderItem.Amount = amount;

        // Act
        var result = _validator.TestValidate(orderItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Amount);
    }
}
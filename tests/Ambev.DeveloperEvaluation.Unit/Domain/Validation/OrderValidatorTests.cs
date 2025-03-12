using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class OrderValidatorTests
{
    private readonly OrderValidator _validator = new();
    
    [Fact(DisplayName = "Valid order should pass all validation rules")]
    public void Given_ValidOrder_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var order = OrderTestData.GenerateValid();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Theory(DisplayName = "Invalid order number formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("12345678910")] // Greater than 10 characters
    public void Given_InvalidOrderNumber_When_Validated_Then_ShouldHaveError(string orderNumber)
    {
        // Arrange
        var order = OrderTestData.GenerateValid();
        order.OrderNumber = orderNumber;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OrderNumber);
    }
    
    [Theory(DisplayName = "Invalid order branch formats should fail validation")]
    [InlineData("")] // Empty
    public void Given_InvalidOrderBranch_When_Validated_Then_ShouldHaveError(string branch)
    {
        // Arrange
        var order = OrderTestData.GenerateValid();
        order.Branch = branch;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Branch);
    }
    
    [Theory(DisplayName = "Invalid order amount formats should fail validation")]
    [InlineData(-10.0)] // less than 1
    [InlineData(-1)] // less than 1
    [InlineData(0)] // less than 1
    public void Given_InvalidOrderAmount_When_Validated_Then_ShouldHaveError(decimal amount)
    {
        // Arrange
        var order = OrderTestData.GenerateValid();
        order.TotalAmount = amount;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
    }
    
    [Fact(DisplayName = "Invalid order item count formats should fail validation")]
    public void Given_InvalidOrderItemCount_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValid();
        order.Items.Clear();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }
}
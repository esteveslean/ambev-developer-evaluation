using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.MessageBroker.Consumers;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class OrderTests
{
    [Fact(DisplayName = "Validation should pass for valid order data")]
    public void Given_ValidOrder_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        int orderItemCount = 10;
        var order = OrderTestData.GenerateValid(orderItemCount);

        // Act
        var result = order.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact(DisplayName = "Validation should fail for invalid order data")]
    public void Given_InvalidOrder_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var order = new Order()
        {
            OrderNumber = "", // Invalid: empty
            Branch = "", // Invalid: empty
            UserId = Guid.Empty, // Invalid: empty
            TotalAmount = 0,
            Items = [], // Invalid: empty
        };

        // Act
        var result = order.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
    
    [Fact(DisplayName = "AddProduct_MaximumAmountRestrictions_ShouldThrowException")]
    public void AddProduct_MaximumAmountRestrictions_ShouldThrowException()
    {
        var order = OrderTestData.GenerateValid(0);

        var action = () => order.AddItem(new OrderItem(Guid.NewGuid(), 21, 123.45m));

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot have more than 20 items per product");
    }

    [Fact(DisplayName = "RaiseDiscount_LessThan4Items_ShouldNotApplyDiscount")]
    public void RaiseDiscount_LessThan4Items_ShouldNotApplyDiscount()
    {
        // Arrange
        var orderItem = new OrderItem(Guid.NewGuid(), 3, 10.00m);
  
        // Act
        orderItem.RaiseDiscount();
        
        // Assert
        orderItem.TotalPrice.Should().Be(30.00m);
        orderItem.Discount.Should().Be(0);
    }
    
    [Theory(DisplayName = "RaiseDiscount_ShouldApplyDiscount")]
    [InlineData(4, 0.1)]
    [InlineData(10, 0.2)]
    public void RaiseDiscount_ShouldApplyDiscount(int amount, decimal expectedDiscount)
    {
        // Arrange
        decimal unitPrice = 10;
        var orderItem = new OrderItem(Guid.NewGuid(), amount, unitPrice);
  
        // Act
        orderItem.RaiseDiscount();

        // Assert
        orderItem.Discount.Should().Be(expectedDiscount);
    }
    
    [Fact(DisplayName = "Cancel_ShouldMarkSaleAsCancelled")]
    public void Cancel_ShouldMarkSaleAsCancelled()
    {
        // Arrange
        Order order = OrderTestData.GenerateValid();

        // Act
        order.Cancel();

        // Assert
        order.IsCancelled.Should().BeTrue();
    }
    
    [Fact(DisplayName = "CancelItem_ShouldMarkSaleAsCancelled")]
    public void CancelItem_ShouldMarkSaleAsCancelled()
    {
        // Arrange
        Order order = OrderTestData.GenerateValid(1);
        OrderItem item = order.Items[0];

        // Act
        order.CancelItem(item.Id);

        // Assert
        order.Items.Should().BeEmpty();
        order.IsCancelled.Should().BeTrue();
    }
}

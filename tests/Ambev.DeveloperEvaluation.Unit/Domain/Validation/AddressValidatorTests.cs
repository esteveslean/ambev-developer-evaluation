using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;


public class AddressValidatorTests
{
    private readonly AddressValidator _validator = new();

    [Fact(DisplayName = "Valid address should pass all validation rules")]
    public void Given_ValidAddress_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();

        // Act
        var result = _validator.TestValidate(user.Address);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Theory(DisplayName = "Invalid address city formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("la")] // Less than 3 characters
    [InlineData("abcdfeghijklmnopqrstuvzx123456789 abcdfeghijklmnopqrstuvzx123456789")] // Greater than 65 characters
    public void Given_InvalidCity_When_Validated_Then_ShouldHaveError(string city)
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Address.City = city;

        // Act
        var result = _validator.TestValidate(user.Address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City);
    }
    
    [Theory(DisplayName = "Invalid address street formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("la")] // Less than 3 characters
    [InlineData("abcdfeghijklmnopqrstuvzx123456789 abcdfeghijklmnopqrstuvzx123456789 abcdfeghijklmnopqrstuvzx123456789 abcdfeghijklmnopqrstuvzx123456789")] // Greater than 65 characters
    public void Given_InvalidStreet_When_Validated_Then_ShouldHaveError(string street)
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Address.Street = street;

        // Act
        var result = _validator.TestValidate(user.Address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }
    
    [Theory(DisplayName = "Invalid address number formats should fail validation")]
    [InlineData(-1)] // Greater than 0
    [InlineData(0)] // Greater than 0
    [InlineData(10000)] // less than 10000 characters
    public void Given_InvalidNumber_When_Validated_Then_ShouldHaveError(int number)
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Address.Number = number;

        // Act
        var result = _validator.TestValidate(user.Address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Number);
    }
}

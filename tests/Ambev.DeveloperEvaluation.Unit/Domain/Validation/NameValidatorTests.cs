using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;


public class NameValidatorTests
{
    private readonly NameValidator _validator = new();

    [Fact(DisplayName = "Valid name should pass all validation rules")]
    public void Given_ValidName_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();

        // Act
        var result = _validator.TestValidate(user.Name);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

  
    [Theory(DisplayName = "Invalid firstname formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidFirstname_When_Validated_Then_ShouldHaveError(string firstname)
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Name.Firstname = firstname;

        // Act
        var result = _validator.TestValidate(user.Name);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Firstname);
    }
    
    [Theory(DisplayName = "Invalid lastname formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidLastname_When_Validated_Then_ShouldHaveError(string lastname)
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Name.Lastname = lastname;

        // Act
        var result = _validator.TestValidate(user.Name);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Lastname);
    }
}

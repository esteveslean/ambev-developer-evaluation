using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class ActiveUserSpecificationTests
    {
        [Theory]
        [InlineData(UserStatus.Active, true)]
        [InlineData(UserStatus.Inactive, false)]
        [InlineData(UserStatus.Suspended, false)]
        public void IsSatisfiedBy_ShouldValidateUserStatus(UserStatus status, bool expectedResult)
        {
            // Arrange
            var user = ActiveUserSpecificationTestData.GenerateUser(status);

            // Act
            var result = UserSpecifications.IsSatisfiedBy(user);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}

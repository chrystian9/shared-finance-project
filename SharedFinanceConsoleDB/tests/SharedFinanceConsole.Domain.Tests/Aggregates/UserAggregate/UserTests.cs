using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;
using SharedFinanceConsoleDB.Domain.Common.DomainException;

namespace SharedFinanceConsoleDB.Domain.Tests.Aggregates.UserAggregate
{
    public class UserTests
    {
        [Fact]
        public void Constructor_ValidName_ShouldSetName()
        {
            // Arrange
            var name = "John Doe";

            // Act
            var user = new User(name);

            // Assert
            Assert.Equal(name, user.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ShouldThrowDomainException(string invalidName)
        {
            // Arrange
            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new User(invalidName));
            Assert.Equal(DomainException.UserNameEmpty, exception.Message);
        }
    }
}

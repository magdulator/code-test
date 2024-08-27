using FluentAssertions;
using TollCalculator.source.Services;

namespace UnitTests.Services
{
    public class TimeIntervarFeeServiceTests
    {
        private readonly TimeIntervalFeeService _timeIntervalFeeBuilder;

        public TimeIntervarFeeServiceTests()
        {
            _timeIntervalFeeBuilder = new TimeIntervalFeeService();
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(5, 59, 59, 0)]
        [InlineData(6, 15, 0, 8)]
        [InlineData(6, 30, 0, 13)]
        [InlineData(7, 0, 0, 18)]
        [InlineData(8, 0, 0, 13)]
        [InlineData(8, 30, 0, 8)]
        [InlineData(15, 0, 0, 13)]
        [InlineData(16, 59, 59, 18)]
        [InlineData(17, 30, 0, 13)]
        [InlineData(18, 29, 59, 8)]
        [InlineData(18, 30, 0, 0)]
        public void GetCurrentFee_ShouldReturnCorrectFee(int hour, int minute, int second, int expectedFee)
        {
            // Arrange
            var dateTime = new DateTime(2024, 8, 27, hour, minute, second);

            // Act
            var fee = _timeIntervalFeeBuilder.GetCurrentFee(dateTime);

            // Assert
            fee.Should().Be(expectedFee);
        }
    }
}
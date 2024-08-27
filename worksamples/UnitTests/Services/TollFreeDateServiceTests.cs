using FluentAssertions;
using TollCalculator.source.Models;
using TollCalculator.source.Services;

namespace UnitTests.Services
{
    public class TollFreeDateServiceTests
    {
        private readonly TollFreeDateService _tollFreeDateService;
        public TollFreeDateServiceTests()
        {
            _tollFreeDateService = new TollFreeDateService();
        }

        [Theory]
        [InlineData(2013, 03, 29, true)] // good friday 2013
        [InlineData(2013, 03, 28, true)] // day before good friday 2013
        [InlineData(2024, 08, 25, true)] // sunday
        [InlineData(2024, 07, 1, true)] // july
        [InlineData(2024, 08, 27, false)] // normal working day
        public void GetTollFreeDates_ReturnsExpectedResult(int year, int month, int day, bool expectedResult)
        {
            // Arrange
            var dateTime = new DateTime(year, month, day);

            // Act
            var result = _tollFreeDateService.IsTollFreeDate(dateTime);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
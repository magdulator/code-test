using FluentAssertions;
using TollCalculator.source.Models;

namespace UnitTests.Services
{
    public class TollCalculatorTests
    {
        [Fact]
        public void GetTollFee_WithinOneHour_ReturnsTheHighestFee()
        {
            // Arrange
            var expectedTotalFee = 13;

            var tollCalculator = new TollCalculatorService();
            DateTime[] dates = {
                new DateTime(2024, 8, 27, 14, 50, 0), // fee: 8
                new DateTime(2024, 8, 27, 15, 15, 0), // fee: 13
            };
            var vehicle = new Car();

            // Act
            var tollFee = tollCalculator.GetTotalTollFee(vehicle, dates);

            // Assert
            tollFee.Should().Be(expectedTotalFee);
        }

        [Fact]
        public void GetTollFee_ManyWithinOneHour_ReturnsTheHighestFee()
        {
            // Arrange
            var expectedTotalFee = 18;

            var tollCalculator = new TollCalculatorService();
            DateTime[] dates = {
                new DateTime(2024, 8, 27, 7, 3, 0), // fee: 18
                new DateTime(2024, 8, 27, 7, 50, 0), // fee: 18
                new DateTime(2023, 8, 27, 8, 0, 0), // fee: 13
                new DateTime(2023, 8, 27, 8, 20, 0), // fee: 13
            };
            var vehicle = new Car();

            // Act
            var tollFee = tollCalculator.GetTotalTollFee(vehicle, dates);

            // Assert
            tollFee.Should().Be(expectedTotalFee);
        }

        [Fact]
        public void GetTollFee_NotWithinOneHour_ReturnsSumOfFees()
        {
            // Arrange
            var expectedTotalFee = 8 + 18 + 8;

            var tollCalculator = new TollCalculatorService();
            DateTime[] dates = {
                new DateTime(2024, 8, 27, 6, 0, 0), // fee: 8
                new DateTime(2024, 8, 27, 7, 1, 0), // fee: 18
                new DateTime(2024, 8, 27, 18, 29, 59), // fee: 8
            };
            var vehicle = new Car();

            // Act
            var tollFee = tollCalculator.GetTotalTollFee(vehicle, dates);

            // Assert
            tollFee.Should().Be(expectedTotalFee);
        }


        [Fact]
        public void GetTollFee_TollFreeVehicle_ReturnsZero()
        {
            // Arrange
            var expectedTotalFee = 0;

            var tollCalculator = new TollCalculatorService();
            DateTime[] dates = {
                    new DateTime(2024, 8, 26, 15, 0, 0)
            };
            var vehicle = new Motorbike();

            // Act
            var tollFee = tollCalculator.GetTotalTollFee(vehicle, dates);

            // Assert
            tollFee.Should().Be(expectedTotalFee);
        }

        [Fact]
        public void GetTollFee_OutsideFeeHours_ReturnsZero()
        {
            // Arrange
            var tollCalculator = new TollCalculatorService();
            DateTime[] dates = {
                new DateTime(2024, 8, 27, 18, 30, 0),
                new DateTime(2024, 8, 27, 0, 0, 0),
                new DateTime(2024, 8, 27, 5, 59, 59)
            };
            var vehicle = new Car();

            // Act
            var tollFee = tollCalculator.GetTotalTollFee(vehicle, dates);

            // Assert
            tollFee.Should().Be(0);
        }

        [Fact]
        public void GetTollFee_PassingMaximumFee_ReturnsMaximumFee()
        {
            // Arrange
            var maximumFeeForADay = 60;
            var tollCalculator = new TollCalculatorService();

            DateTime[] dates = {
                new DateTime(2024, 8, 27, 6, 0, 0), // fee: 8
                new DateTime(2024, 8, 27, 7, 30, 0), // fee: 18
                new DateTime(2024, 8, 27, 9, 0, 0), // fee: 8
                new DateTime(2024, 8, 27, 10, 30, 0), // fee: 8
                new DateTime(2024, 8, 27, 12, 0, 0), // fee: 8
                new DateTime(2024, 8, 27, 13, 30, 0), // fee: 8
                new DateTime(2024, 8, 27, 15, 0, 0), // fee: 13
                new DateTime(2024, 8, 27, 16, 30, 0), // fee: 18
                new DateTime(2024, 8, 27, 18, 00, 0), // fee: 8, total = 97
            };

            var vehicle = new Car();

            // Act
            var tollFee = tollCalculator.GetTotalTollFee(vehicle, dates);

            // Assert
            tollFee.Should().Be(maximumFeeForADay);
        }

        [Fact]
        public void GetTollFee_TollFreeDays_ReturnsZero()
        {
            // Arrange
            var expectedTotalFee = 0;

            var tollCalculator = new TollCalculatorService();
            var dates = GetTollFeeFreeDays2024();

            var vehicle = new Car();

            // Act
            var tollFee = tollCalculator.GetTotalTollFee(vehicle, dates);

            // Assert
            tollFee.Should().Be(expectedTotalFee);
        }

        private static DateTime[] GetTollFeeFreeDays2024()
        {
            var saturday = new DateTime(2024, 1, 6, 8, 0, 0);
            var holiday = new DateTime(2024, 1, 1, 8, 0, 0);
            var dayBeforeHoliday = new DateTime(2024, 5, 8, 8, 0, 0);
            var dayInJuly = new DateTime(2024, 7, 1, 8, 0, 0);

            DateTime[] dates = { saturday, holiday, dayBeforeHoliday, dayInJuly };

            return dates;
        }
    }
}
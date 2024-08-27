using TollCalculator.source.Interfaces;
using TollCalculator.source.Services;

public class TollCalculatorService
{
    private readonly TimeIntervalFeeService _timeIntervalFeeService;
    private readonly TollFreeDateService _tollFreeDaysService;

    private static int DailyMaxTotalFee = 60;

    public TollCalculatorService()
    {
        _timeIntervalFeeService = new TimeIntervalFeeService();
        _tollFreeDaysService = new TollFreeDateService();
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTotalTollFee(IVehicle vehicle, DateTime[] dates)
    {
        DateTime? startOfTollHourTime = null;
        var previousFeeWithinHour = 0;
        var totalFee = 0;
        foreach (var dateTime in dates)
        {
            if (startOfTollHourTime == null || (dateTime - startOfTollHourTime.Value).TotalHours >= 1)
            {
                // add fee as normal if there is no prev passing time or an hour has passed
                var fee = GetTollFee(dateTime, vehicle);
                totalFee += fee;
                startOfTollHourTime = dateTime;
                previousFeeWithinHour = fee;
            }
            else
            {
                var currentFee = GetTollFee(dateTime, vehicle);
                if (currentFee > previousFeeWithinHour)
                {
                    // extract previous fee and add currentFee if it is larger
                    totalFee = totalFee - previousFeeWithinHour + currentFee;
                    previousFeeWithinHour = currentFee;
                }
            }
            if (totalFee > DailyMaxTotalFee)
            {
                return DailyMaxTotalFee;
            }
        }
        return totalFee;
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        return vehicle.IsTollFree;
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (_tollFreeDaysService.IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        return _timeIntervalFeeService.GetCurrentFee(date);
    }
}
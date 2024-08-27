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
        DateTime? lastPassedTollTime = dates[0];
        var totalFee = 0;
        foreach (var dateTime in dates)
        {
            if (startOfTollHourTime == null || (dateTime - startOfTollHourTime.Value).TotalHours >= 1)
            {
                totalFee += GetTollFee(dateTime, vehicle);
                startOfTollHourTime = dateTime;
            }
            else
            {
                var currentFee = GetTollFee(dateTime, vehicle);
                var previousFee = GetTollFee(lastPassedTollTime.Value, vehicle);

                if (currentFee > previousFee)
                {
                    totalFee = totalFee - previousFee + currentFee;
                }
            }
            if (totalFee > DailyMaxTotalFee)
            {
                return DailyMaxTotalFee;
            }
            lastPassedTollTime = dateTime;
           
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
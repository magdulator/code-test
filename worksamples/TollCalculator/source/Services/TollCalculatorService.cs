using TollCalculator.source.Interfaces;
using TollCalculator.source.Services;

public class TollCalculatorService
{
    private readonly TimeIntervalFeeService _timeIntervalFeeService;
    private static int DailyMaxTotalFee = 60;

    public TollCalculatorService()
    {
        _timeIntervalFeeService = new TimeIntervalFeeService();
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
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        return _timeIntervalFeeService.GetCurrentFee(date);
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}
using PublicHoliday;
using System.Linq.Expressions;
namespace TollCalculator.source.Services
{
    
    public class TollFreeDateService
    {
        private readonly SwedenPublicHoliday _swedishPublicHolidays;

        public TollFreeDateService()
        {
            _swedishPublicHolidays = new SwedenPublicHoliday();
        }

        public bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || 
                date.DayOfWeek == DayOfWeek.Sunday ||
                date.Month == 7 || 
                IsHolidayOrDayBeforeHoliday(date);
        }

        private bool IsHolidayOrDayBeforeHoliday(DateTime date) 
        {
            return _swedishPublicHolidays.IsPublicHoliday(date) ||
                _swedishPublicHolidays.IsPublicHoliday(date.AddDays(1));
        }
    }
}

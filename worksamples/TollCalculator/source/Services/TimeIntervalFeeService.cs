namespace TollCalculator.source.Services
{
    public class TimeIntervalFeeService
    {
        private readonly List<TimeIntervalFee> TimeIntervalFeeList;

        public TimeIntervalFeeService()
        {
            TimeIntervalFeeList = CreateTimeIntervalFeeList();
        }

        public int GetCurrentFee(DateTime dateTime)
        {
            foreach (var intervalFee in TimeIntervalFeeList)
            {
                if (dateTime.TimeOfDay >= intervalFee.StartTime && dateTime.TimeOfDay < intervalFee.EndTime)
                {
                    return intervalFee.Fee;
                }
            }
            return 0;
        }

        private static List<TimeIntervalFee> CreateTimeIntervalFeeList()
        {
            return new List<TimeIntervalFee>() {
                new TimeIntervalFee { StartTime = new TimeSpan(06, 00, 00), EndTime = new TimeSpan(06, 30, 00), Fee = 8 },
                new TimeIntervalFee { StartTime = new TimeSpan(06, 30, 00), EndTime = new TimeSpan(07, 00, 00), Fee = 13 },
                new TimeIntervalFee { StartTime = new TimeSpan(07, 00, 00), EndTime = new TimeSpan(08, 00, 00), Fee = 18 },
                new TimeIntervalFee { StartTime = new TimeSpan(08, 00, 00), EndTime = new TimeSpan(08, 30, 00), Fee = 13 },
                new TimeIntervalFee { StartTime = new TimeSpan(08, 30, 00), EndTime = new TimeSpan(15, 00, 00), Fee = 8 },
                new TimeIntervalFee { StartTime = new TimeSpan(15, 00, 00), EndTime = new TimeSpan(15, 30, 00), Fee = 13 },
                new TimeIntervalFee { StartTime = new TimeSpan(15, 30, 00), EndTime = new TimeSpan(17, 00, 00), Fee = 18 },
                new TimeIntervalFee { StartTime = new TimeSpan(17, 00, 00), EndTime = new TimeSpan(18, 00, 00), Fee = 13 },
                new TimeIntervalFee { StartTime = new TimeSpan(18, 00, 00), EndTime = new TimeSpan(18, 30, 00), Fee = 8 },
            };
        }
    }

    public class TimeIntervalFee
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Fee { get; set; }
    }
}
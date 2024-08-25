using TollCalculator.source.Interfaces;

namespace TollCalculator.source.Models
{
    public class Foreign : IVehicle
    {
        public string VehicleType => "Foreign";
        public bool IsTollFree => true;
    }
}

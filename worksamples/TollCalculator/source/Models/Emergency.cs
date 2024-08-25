using TollCalculator.source.Interfaces;

namespace TollCalculator.source.Models
{
    public class Emergency : IVehicle
    {
        public string VehicleType => "Emergency";
        public bool IsTollFree => true;
    }
}

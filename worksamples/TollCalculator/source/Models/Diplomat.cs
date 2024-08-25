using TollCalculator.source.Interfaces;

namespace TollCalculator.source.Models
{
    public class Diplomat : IVehicle
    {
        public string VehicleType => "Diplomat";
        public bool IsTollFree => true;
    }
}

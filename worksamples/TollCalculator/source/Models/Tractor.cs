using TollCalculator.source.Interfaces;

namespace TollCalculator.source.Models
{
    public class Tractor : IVehicle
    {
        public string VehicleType => "Tractor";
        public bool IsTollFree => true;
    }
}

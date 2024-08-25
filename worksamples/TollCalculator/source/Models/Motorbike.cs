using TollCalculator.source.Interfaces;

namespace TollCalculator.source.Models
{
    public class Motorbike : IVehicle
    {
        public string VehicleType => "Motorbike";
        public bool IsTollFree => true;
    }
}

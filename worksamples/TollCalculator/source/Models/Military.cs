using TollCalculator.source.Interfaces;

namespace TollCalculator.source.Models
{
    public class Military : IVehicle
    {
        public string VehicleType => "Military";
        public bool IsTollFree => true;
    }
}
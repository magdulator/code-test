using TollCalculator.source.Interfaces;

namespace TollCalculator.source.Models
{
    public class Car : IVehicle
    {
        public string VehicleType => "Military";
        public bool IsTollFree => false;
    }
}
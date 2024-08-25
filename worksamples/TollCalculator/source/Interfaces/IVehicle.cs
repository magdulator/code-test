namespace TollCalculator.source.Interfaces
{
    public interface IVehicle
    {
        string VehicleType { get; }
        bool IsTollFree { get; }
    }
}
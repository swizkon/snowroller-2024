namespace CongestionTaxApi.Domain;

public class Vehicle(VehicleType vehicleType) : IVehicle
{
    public static Vehicle Car() => new(VehicleType.Car);

    public static Vehicle Motorcycle() => new(VehicleType.Motorcycle);

    public VehicleType GetVehicleType() => vehicleType;
}
namespace CongestionTaxApi.Domain;

public class Vehicle(VehicleType vehicleType) : IVehicle
{
    public static Vehicle Car() => new(VehicleType.Other);

    public static Vehicle Motorcycle() => new(VehicleType.Motorcycle);

    public VehicleType GetVehicleType() => vehicleType;
}
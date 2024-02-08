using CongestionTaxApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxApi.Api;

public static class EndPointsConfiguration
{
    public static void MapEndpoints(this WebApplication app)
    {
        // Default some kind of "man page" as default for the API
        app.MapGet("/", () =>
            Results.Text(
                content: File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "HELP.md")),
                contentType: "text/plain"));

        // Handle first version of the calculation without city
        app.MapPost("api/congestion-tax/summary", (
            [FromServices] CongestionTaxCalculator calculator,
            [FromBody] CongestionTaxRequest request) =>
        {
            // TODO Validate that the timestamps are all within in the same day?
            // A nice result would be to group days to enable 

            if (!Enum.TryParse(request.Vehicle.VehicleType, out VehicleType vehicleType))
                return Results.BadRequest("Invalid vehicle type");

            if (request.Passages.Any(x => x.Year != 2013))
                return Results.BadRequest("Invalid year. Only support for 2013 right now...");

            var vehicle = new Vehicle(vehicleType);
            var fee = calculator.GetTax(vehicle, request.Passages.ToArray());
            return Results.Ok(fee);
        });
    }
}
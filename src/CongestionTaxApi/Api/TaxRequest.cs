namespace CongestionTaxApi.Api;

public class CongestionTaxRequest
{
    public VehicleRequest Vehicle { get; set; }

    public IEnumerable<DateTime> Passages { get; set; }
}
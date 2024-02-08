namespace CongestionTaxApi.Domain;

public class FeeSettings
{
    public string City { get; set; }
    public Money MaxFeePerDay { get; set; }
    public List<FeeInterval>? FeeIntervals { get; set; }
    
    /// <summary>
    /// Defines the dates when the toll is free for a specific city
    /// </summary>
    public string[] FreeDates { get; set; }
}
namespace CongestionTaxApi.Domain;

public record struct Money(decimal Amount, string Currency)
{
    public static Money Zero(string currency) => new(0, currency);
    public static Money SEK(decimal amount) => new(amount, "SEK");
}
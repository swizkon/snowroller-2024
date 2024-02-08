namespace CongestionTaxApi.Domain;

public static class DecimalExtensions
{
    public static Money ToSEK(this decimal value) => new(value, "SEK");
    public static Money ToSEK(this int value) => new(value, "SEK");
}
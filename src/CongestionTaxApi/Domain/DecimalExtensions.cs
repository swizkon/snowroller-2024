namespace CongestionTaxApi.Domain;

public static class DecimalExtensions
{
    public static Money ToSEK(this decimal value) => new(value, "SEK");
}
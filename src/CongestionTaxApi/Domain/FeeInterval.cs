namespace CongestionTaxApi.Domain;

public record FeeInterval(
    TimeOnly From,
    TimeOnly Until,
    Money Fee);
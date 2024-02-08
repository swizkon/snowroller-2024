namespace CongestionTaxApi.Domain;

public interface ICheckHolidays
{
    bool IsPublicHoliday(string country, DateOnly date);
}
using CongestionTaxApi.Integrations;

namespace CongestionTaxApi.Domain;

public class CongestionTaxCalculator
{
    private readonly ICheckHolidays _holidayCalendar = new NationalCalendarService();

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total congestion tax for that day
     */

    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(DateOnly.FromDateTime(date)) || IsTollFreeVehicle(vehicle)) return 0;

        var hour = date.Hour;
        var minute = date.Minute;

        switch (hour)
        {
            case 6 when minute >= 0 && minute <= 29:
                return 8;
            case 6 when minute >= 30 && minute <= 59:
                return 13;
            case 7 when minute >= 0 && minute <= 59:
                return 18;
            case 8 when minute >= 0 && minute <= 29:
                return 13;
            case >= 8 and <= 14 when minute >= 30 && minute <= 59:
                return 8;
            case 15 when minute >= 0 && minute <= 29:
                return 13;
            case 15 when minute >= 0:
            case 16 when minute <= 59:
                return 18;
            case 17 when minute >= 0 && minute <= 59:
                return 13;
            case 18 when minute >= 0 && minute <= 29:
                return 8;
            default:
                return 0;
        }
    }

    private bool IsTollFreeDate(DateOnly date)
    {
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return true;
        }
        
        var isHoliday = _holidayCalendar.IsPublicHoliday("Sweden", date);
        return isHoliday;
    }

    private static bool IsTollFreeVehicle(IVehicle? vehicle)
    {
        if (vehicle == null) return false;
        var vehicleType = vehicle.GetVehicleType();
        return (vehicleType is
                VehicleType.Motorcycle
                or VehicleType.Tractor
                or VehicleType.Emergency
                or VehicleType.Diplomat
                or VehicleType.Foreign
                or VehicleType.Military
            );
    }

    //private enum TollFreeVehicles
    //{
    //    Motorcycle = 0,
    //    Tractor = 1,
    //    Emergency = 2,
    //    Diplomat = 3,
    //    Foreign = 4,
    //    Military = 5
    //}
}
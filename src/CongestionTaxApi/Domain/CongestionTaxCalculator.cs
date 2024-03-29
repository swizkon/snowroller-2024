using CongestionTaxApi.Configuration;
using CongestionTaxApi.Integrations;

namespace CongestionTaxApi.Domain;

public class CongestionTaxCalculator
{
    private readonly ICheckHolidays _holidayCalendar = new NationalCalendarService();

    private readonly FeeSettings _feeSettings = FeeSettingsConfigs.GetGothenburgFeeSettings(); // Use DI or some other means to configure this

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total congestion tax for that day
     */

    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        // TODO Should we validate here that the dates are from the same day?

        // Calc every passage
        var datesAndRates = dates.Select(date => (date, GetTollFee(date, vehicle))).ToList();
        // Here we need to recurse through the list until no entries are closer that threshold...

        // Order by cost or do a linked list a la king of the hill?

        // TODO Here we need to calculate the price for all the dates and exclude all that has one higher value within the timeframe.
        // 

        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            // TODO This does not do the trick..
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

        // TODO Fix magic number to be from a settings file
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(DateOnly.FromDateTime(date)) || IsTollFreeVehicle(vehicle)) return 0;

        var hour = date.Hour;
        var minute = date.Minute;

        // Get the correct fee for the date time from the fee settings
        var fee = GetFee(TimeOnly.FromDateTime(date));

        // TODO Need to be fixed after tests are stable. Probably better to use decimal or a Money-type everywhere inside the domain...
        return (int)fee.Amount;

        //switch (hour)
        //{
        //    case 6 when minute >= 0 && minute <= 29:
        //        return 8;
        //    case 6 when minute >= 30 && minute <= 59:
        //        return 13;
        //    case 7 when minute >= 0 && minute <= 59:
        //        return 18;
        //    case 8 when minute >= 0 && minute <= 29:
        //        return 13;
        //    case >= 8 and <= 14 when minute >= 30 && minute <= 59:
        //        return 8;
        //    case 15 when minute >= 0 && minute <= 29:
        //        return 13;
        //    case 15 when minute >= 0:
        //    case 16 when minute <= 59:
        //        return 18;
        //    case 17 when minute >= 0 && minute <= 59:
        //        return 13;
        //    case 18 when minute >= 0 && minute <= 29:
        //        return 8;
        //    default:
        //        return 0;
        //}
    }

    private Money GetFee(TimeOnly time)
    {
        var feeIntervals = _feeSettings.FeeIntervals;
        if (feeIntervals == null) return 0.ToSEK();

        // Get a single fee that matches the time
        var interval = feeIntervals.SingleOrDefault(feeInterval =>
                       feeInterval.From <= time && time < feeInterval.Until);

        return interval?.Fee ?? 0.ToSEK();
    }

    private bool IsTollFreeDate(DateOnly date)
    {
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return true;
        }

        // Hard-coded rule for now...
        if (date.Month is 7)
        {
            return true;
        }

        var isHoliday = _holidayCalendar.IsPublicHoliday("Sweden", date);
        return isHoliday;
    }

    private static bool IsTollFreeVehicle(IVehicle? vehicle)
    {
        // Here maybe a combo of vehicle category and type would be better
        // For example a car or motorcycle could be a foreign vehicle
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
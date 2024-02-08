using CongestionTaxApi.Configuration;
using CongestionTaxApi.Domain;
using FluentAssertions;

namespace CongestionTaxApi.Tests;

public class CongestionTaxApiTests
{
    public CongestionTaxCalculator CongestionTaxCalculator => new CongestionTaxCalculator();

    [Theory]
    [InlineData("2013-12-10 05:59:59", 0)]
    [InlineData("2013-12-10 06:00", 8)]
    [InlineData("2013-12-10 14:59:59", 8)]
    [InlineData("2013-12-10 15:00", 13)]
    [InlineData("2013-12-10 17:59:59.999", 13)]
    [InlineData("2013-12-10 18:30", 0)]
    public void Given_default_fee_settings_and_single_passage_Then_tax_should_be_correct(DateTime date, int expectedTax)
    {
        var result = CongestionTaxCalculator.GetTax(Vehicle.Car(), new[]
        {   
            date
        });

        result.Should().Be(expectedTax);
    }

    [Fact]
    public void Weekends_are_TollFree()
    {
        var date = GetWeekendDate();

        var result = CongestionTaxCalculator.GetTollFee(date, Vehicle.Car());

        result.Should().Be(0);
    }

    [Fact]
    public void ChristmasEve_is_TollFree()
    {
        var date = DateTime.Parse("2013-12-24 15:00");

        var result = CongestionTaxCalculator.GetTollFee(date, Vehicle.Car());

        result.Should().Be(0);
    }

    [Fact]
    public void July_is_TollFree()
    {
        Enumerable.Range(1, 31)
            .Select(day => new DateTime(2013, 7, day, 15, 15, 15))
            .Select(date => CongestionTaxCalculator.GetTollFee(date, Vehicle.Car()))
            .Should()
            .AllBeEquivalentTo(0);
    }

    [Fact]
    public void Given_many_passages_Max_is_picked_from_fee_settings()
    {
        var passages =  Enumerable.Range(6, 14)
            .Select(hour => new DateTime(2013, 12, 10, hour, 15, 15));

        var result = CongestionTaxCalculator.GetTax(Vehicle.Car(), passages.ToArray());
        result.Should().Be(60);
    }

    [Fact]
    public void Motorbikes_are_TollFree()
    {
        var date = DateTime.Parse("2013-12-20 15:00");

        var result = CongestionTaxCalculator.GetTollFee(date, Vehicle.Motorcycle());

        result.Should().Be(0);
    }

    [Fact]
    public void It_should_not_have_gaps()
    {
        var date = DateTime.Parse("2013-12-20 17:59:37");

        var result = CongestionTaxCalculator.GetTollFee(date, Vehicle.Car());

        result.Should().Be(13);
    }

    private DateTime GetWeekendDate()
    {
        // Maybe we should use a random date instead of hard-coding this...

        var date = new DateTime(2013, 1, 1)
                    .AddDays(Random.Shared.Next(10, 360));
        while (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
        {
            date = date.AddDays(Random.Shared.Next(-2, 0));
        }
        return date;
    }

    [Fact]
    public Task It_should_handle_every_known_vehicle_and_date()
    {
        // A bit massive maybe...
        var passesByDay = DatesOfInterest
            .Select(DateTime.Parse)
            .OrderBy(x => x)
            .GroupBy(DateOnly.FromDateTime)
            .ToDictionary(group => group.Key, group => group.ToList());

        var fees = new List<string>();
        foreach (var (date, dates) in passesByDay)
        {
            var vehicle = Vehicle.Car();
            var result = CongestionTaxCalculator.GetTax(vehicle, dates.ToArray());
            fees.Add($"{date:yyyy-MM-dd} is a {date.DayOfWeek} and {vehicle.GetVehicleType()} should pay {result}");
        }

        return Verify(fees);
    }

    [Fact]
    public Task It_should_handle_every_known_vehicle_at_a_single_timestamp()
    {
        // A bit massive maybe...
        var dates = DatesOfInterest
            .Select(DateTime.Parse)
            .OrderBy(x => x);

        var fees = new List<string>();

        var vehicle = Vehicle.Car();
        foreach (var date in dates)
        {
            var result = CongestionTaxCalculator.GetTollFee(date, vehicle);
            fees.Add($"{date:s} is a {date.DayOfWeek} and {vehicle.GetVehicleType()} should pay {result}");
        }

        return Verify(fees);
    }

    private readonly IList<string> DatesOfInterest = new List<string>
    {
        "2013-01-14 21:00:00",
        "2013-01-15 21:00:00",
        "2013-02-07 06:23:27",
        "2013-02-07 15:27:00",
        "2013-02-08 06:27:00",
        "2013-02-08 06:20:27",

        "2013-02-08 14:35:00", // These need to be tested properly for correct toll fees grouped per hour
        "2013-02-08 15:29:00", // ----^---------------^----------------------
        "2013-02-08 15:47:00", // 
        "2013-02-08 16:01:00",
        "2013-02-08 16:48:00",
        "2013-02-08 17:49:00", // In its own group

        "2013-02-08 18:29:00", // <- Need to 
        "2013-02-08 18:35:00",
        "2013-03-26 14:25:00",
        "2013-03-28 14:07:27",

        "2013-12-24 15:00:00",
    };

}

using CongestionTaxApi.Domain;

namespace CongestionTaxApi.Configuration;

public static class FeeSettingsConfigs
{
    public static FeeSettings GetGothenburgFeeSettings()
    {
        return new FeeSettings
        {
            MaxFeePerDay = 60m.ToSEK(),
            City = "Gothenburg",

            // TODO Implement logic for free dates
            FreeDates = new []{ "2013-07-\\d{2}" },

            FeeIntervals = new List<FeeInterval>
            {
                // Should "until" really be set as minute before or just back-to-back to enable simpler logic?

                // For example, a store that is opened from 10:00-20:00.
                // Would customers expect store to be open until 20:01?

                new(From: TimeOnly.Parse("06:00"), Until: TimeOnly.Parse("06:30"), Fee: 8.ToSEK()),
                new(From: TimeOnly.Parse("06:30"), Until: TimeOnly.Parse("07:00"), Fee: 13.ToSEK()),
                new(From: TimeOnly.Parse("07:00"), Until: TimeOnly.Parse("08:00"), Fee: 18.ToSEK()),
                new(From: TimeOnly.Parse("08:00"), Until: TimeOnly.Parse("08:30"), Fee: 13.ToSEK()),
                new(From: TimeOnly.Parse("08:30"), Until: TimeOnly.Parse("15:00"), Fee: 8.ToSEK()),
                new(From: TimeOnly.Parse("15:00"), Until: TimeOnly.Parse("15:30"), Fee: 13.ToSEK()),
                new(From: TimeOnly.Parse("15:30"), Until: TimeOnly.Parse("17:00"), Fee: 18.ToSEK()),

                new(From: TimeOnly.Parse("17:00"), Until: TimeOnly.Parse("18:00"), Fee: 13.ToSEK()),
                new(From: TimeOnly.Parse("18:00"), Until: TimeOnly.Parse("18:30"), Fee: 8.ToSEK()),

                // Examples of invalid overlapping config
                //new(From: TimeOnly.Parse("23:59:50"), Until: TimeOnly.Parse("23:59:53"), Fee: 1000.ToSEK()),
                //new(From: TimeOnly.Parse("23:59:51"), Until: TimeOnly.Parse("23:59:55"), Fee: 1000.ToSEK()),
            }
        };
    }
}
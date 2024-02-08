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

            FreeDates = new []{ "2013-07-\\d{2}" },

            FeeIntervals = new List<FeeInterval>
            {
                // Should "until" really be set as minute before or just back-to-back to enable simpler logic?

                // For example, a store that is opened from 10:00-20:00.
                // Would customers expect store to be open until 20:01?

                new(From: TimeOnly.Parse("06:00"), Until: TimeOnly.Parse("06:29"), Fee: 8m.ToSEK()),
                new(From: TimeOnly.Parse("06:30"), Until: TimeOnly.Parse("06:59"), Fee: 13m.ToSEK()),
                new(From: TimeOnly.Parse("07:00"), Until: TimeOnly.Parse("07:59"), Fee: 18m.ToSEK()),
                new(From: TimeOnly.Parse("08:00"), Until: TimeOnly.Parse("08:29"), Fee: 13m.ToSEK()),
                new(From: TimeOnly.Parse("08:30"), Until: TimeOnly.Parse("14:59"), Fee: 8m.ToSEK()),
                new(From: TimeOnly.Parse("15:00"), Until: TimeOnly.Parse("15:29"), Fee: 13m.ToSEK()),
                new(From: TimeOnly.Parse("15:30"), Until: TimeOnly.Parse("16:59"), Fee: 18m.ToSEK()),
                new(From: TimeOnly.Parse("17:00"), Until: TimeOnly.Parse("17:59"), Fee: 13m.ToSEK()),
                new(From: TimeOnly.Parse("18:00"), Until: TimeOnly.Parse("18:29"), Fee: 8m.ToSEK())
            }
        };
    }
}
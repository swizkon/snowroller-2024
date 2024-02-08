using CongestionTaxApi.Domain;

namespace CongestionTaxApi.Integrations;

public class NationalCalendarService : ICheckHolidays
{
    public bool IsPublicHoliday(string country, DateOnly date)
    {
        // Only consider 2013 for now...
        if(date.Year != 2013)
        {
            return false;
        }

        return country.ToLower() switch
        {
            "sweden" => IsSwedishHoliday(date),
            _ => false
        };
    }

    private static bool IsSwedishHoliday(DateOnly date)
    {
        // This should maybe be a config or a service call

        // Sources:
        // https://www.kalender.se/aftnar/2013
        // https://www.kalender.se/helgdagar/2013

        // Maybe not totally reliable, but good enough for now...

        return new[]
            {
                "2013-01-01", // Nyårsdagen	1	Tisdag	1
                "2013-01-05", // Trettondagsafton	1	Söndag	6
                "2013-01-06", // Trettondedag jul    1	Söndag	6

                "2013-03-28", // Skärtorsdag	13	Torsdag	87
                "2013-03-29", // Långfredagen	13	Fredag	88
                "2013-03-30", // Påskafton	13	Lördag	89
                "2013-03-31", // Påskdagen	13	Söndag	90

                "2013-04-01", // Annandag påsk   14	Måndag	91
                "2013-04-30", // Valborgsmässoafton	18	Tisdag	120

                "2013-05-01", // Första maj  18	Onsdag	121
                "2013-05-09", // Kristi himmelfärdsdag   19	Torsdag	129
                "2013-05-18", // Pingstafton	20	Lördag	138
                "2013-05-19", // Pingstdagen	20	Söndag	139

                "2013-06-06", // Sveriges nationaldag    23	Torsdag	157
                "2013-06-21", // Midsommarafton	25	Fredag	172
                "2013-06-22", // Midsommardagen	25	Lördag	173

                "2013-11-01", // Alla helgons afton	44	Fredag	305
                "2013-11-02", // Alla helgons dag	44	Lördag	306

                "2013-12-24", // Julafton	52	Tisdag	358
                "2013-12-25", // Juldagen	52	Onsdag	359
                "2013-12-26", // Annandag jul    52	Torsdag
                "2013-12-31", // Nyårsafton	1	Tisdag
            }
            .Select(DateOnly.Parse)
            .Contains(date);
    }
}


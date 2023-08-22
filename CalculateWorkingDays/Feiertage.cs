namespace CalculateWorkingDays;

public class Feiertage
{

    public static List<DateTime> GetFeiertage()
    {
        var feiertage = new List<DateTime>();

        for (int year = 2019; year <= 2023; year++)
        {
            feiertage.Add(new DateTime(year, 1, 1));    // Neujahr
            feiertage.Add(new DateTime(year, 5, 1));    // Tag der Arbeit
            feiertage.Add(new DateTime(year, 10, 3));   // Tag der dt Einheit
            feiertage.Add(new DateTime(year, 11, 1));   // Allerheiligen
            feiertage.Add(new DateTime(year, 12, 25));  // 1. Weihnachtstag
            feiertage.Add(new DateTime(year, 12, 26));  // 2. Weihnachtstag
            feiertage.Add(new DateTime(year, 12, 31));  // Silvester
            
            // weitere Bewegliche Feiertage mit Osterformel
            var osterSonntag = CalculateOstern(year);
            var karfreitag = osterSonntag.AddDays(-1);
            feiertage.Add(karfreitag);
            var ostermontag = osterSonntag.AddDays(1);
            feiertage.Add(ostermontag);

            var christihimmelfahrt = osterSonntag.AddDays(40);
            feiertage.Add(christihimmelfahrt);

            var pfingstsonntag = osterSonntag.AddDays(50);
            var pfingstmontag = osterSonntag.AddDays(51);
            feiertage.Add(pfingstmontag);

            var fronleichnam = osterSonntag.AddDays(60);
            feiertage.Add(fronleichnam);
        }

        return feiertage;
    }

    static DateTime CalculateOstern(int year)
    {
            int c = year / 100;
            int n = year - 19 * (year / 19);
            int k = (c - 17) / 25;
            int i = c - c / 4 - ((c - k) / 3) + 19 * n + 15;
            i = i - 30 * (i / 30);
            i = i - (i / 28) * ((1 - (i / 28)) * (29 / (i + 1)) * ((21 - n) / 11));
            int j = year + (year / 4) + i + 2 - c + (c / 4);
            j = j - 7 * (j / 7);
            int l = i - j;
 
            int easterMonth = 3 + ((l + 40) / 44);
            int easterDay = l + 28 - 31 * (easterMonth / 4);

            return new DateTime(year, easterMonth, easterDay);
    }
    
}
    
    
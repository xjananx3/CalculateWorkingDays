﻿
using System.Globalization;
using CalculateWorkingDays;
using CsvHelper;
using CsvHelper.Configuration;

class Program
{
    static void Main(string[] args)
    {
        string inputFilePath = @"D:\Test\Mappe1.csv";
        string outputFilePath = @"D:\Test\Mappe1337.csv";

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };
        
        
        using (var reader = new StreamReader(inputFilePath))
        using (var csv = new CsvReader(reader, config))
        {
            var eintrage = csv.GetRecords<Zeiten>();

            using (var writer = new StreamWriter(outputFilePath))
            using (var csvWriter = new CsvWriter(writer, config))
            {
                csvWriter.WriteHeader<Zeiten>();
                csvWriter.NextRecord();
                
                foreach (var eintrag in eintrage)
                {
                    int days = CalculateWorkingDays(eintrag.StartDate, eintrag.EndDate);
                    csvWriter.WriteField(eintrag.StartDate);
                    csvWriter.WriteField(eintrag.EndDate);
                    csvWriter.WriteField(days);
                    csvWriter.NextRecord();
                }
            }
        }
    }

    static int CalculateWorkingDays(DateTime startDate, DateTime endDate)
    {
        int weekdays = 0;
        var feiertagsListe = Feiertage.GetFeiertage();
        
        while (startDate <= endDate)
        {
            if (startDate.DayOfWeek != DayOfWeek.Saturday 
                && startDate.DayOfWeek != DayOfWeek.Sunday && !feiertagsListe.Contains(startDate))
            {
                weekdays++;
            }
            startDate = startDate.AddDays(1);
        }
        return weekdays;
    }
}

public record Zeiten(DateTime StartDate, DateTime EndDate, int Days);

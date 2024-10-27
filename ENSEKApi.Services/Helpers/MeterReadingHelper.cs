using CsvHelper;
using CsvHelper.Configuration;
using ENSEKApi.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace ENSEKApi.Services.Helpers;

public static class MeterReadingHelper
{
    public static async Task<MeterReadingCsv> ConvertCsvFileToMeterReadingsAsync(IFormFile file)
    {
        var meterReadings = new List<MeterReading>();
        int invalidReadings = 0;

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.Context.RegisterClassMap<MeterReadingMap>();

            while (await csv.ReadAsync())
            {
                try
                {
                    meterReadings.Add(csv.GetRecord<MeterReading>());
                }
                catch
                {
                    invalidReadings++;
                }
            }
        }

        return new MeterReadingCsv
        {
            ValidMeterReadings = meterReadings,
            InvalidMeterReadings = invalidReadings
        };
    }
}

using CsvHelper.Configuration;

namespace ENSEKApi.Models;

public class MeterReadingMap : ClassMap<MeterReading>
{
    public MeterReadingMap()
    {
        Map(m => m.AccountId).Name("AccountId");
        Map(m => m.ReadingDateTime).Name("MeterReadingDateTime").TypeConverterOption.Format("dd/MM/yyyy HH:mm");
        Map(m => m.ReadValue).Name("MeterReadValue");
    }
}

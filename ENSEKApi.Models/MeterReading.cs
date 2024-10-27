using CsvHelper.Configuration.Attributes;

namespace ENSEKApi.Models;

public class MeterReading
{
    public int AccountId { get; set; }
   
    public DateTime ReadingDateTime { get; set; }

    public int ReadValue { get; set; }
}

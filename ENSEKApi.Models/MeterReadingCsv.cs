namespace ENSEKApi.Models;

public class MeterReadingCsv
{
    public required List<MeterReading> ValidMeterReadings { get; set; }
    public int InvalidMeterReadings { get; set; }
}

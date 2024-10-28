using ENSEKApi.Models;

namespace ENSEKApi.Domain.Interfaces;

public interface IMeterReadingValidatorService
{
    Task<(List<MeterReading> validReadings, List<MeterReading> invalidReadings)> ValidateAsync(IEnumerable<MeterReading> meterReadings);
}

using ENSEKApi.Models;

namespace ENSEKApi.Domain.Interfaces;

public interface IMeterReadingRepository
{
    Task AddAsync(IEnumerable<MeterReading> meterReadings);
    Task<List<MeterReading>> GetAllAsync();
}

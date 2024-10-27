using ENSEKApi.Models;

namespace ENSEKApi.Domain.Interfaces;

public interface IMeterReadingService
{
    Task<MeterReadingUpdateResult> AddAsync(MeterReadingCsv meterReadingCsv);
}

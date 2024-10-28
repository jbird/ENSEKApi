using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using Microsoft.Extensions.Logging;

namespace ENSEKApi.Services;

public class MeterReadingService(ILogger<MeterReadingService> logger, IMeterReadingRepository meterReadingRepository, IMeterReadingValidatorService validatorService) : IMeterReadingService
{
    private readonly ILogger<MeterReadingService> _logger = logger;
    private readonly IMeterReadingRepository _meterReadingRepository = meterReadingRepository;
    private readonly IMeterReadingValidatorService _validatorService = validatorService;

    public async Task<MeterReadingUpdateResult> AddAsync(MeterReadingCsv meterReadingCsv)
    {
        var (validReadings, invalidReadings) = await _validatorService.ValidateAsync(meterReadingCsv.ValidMeterReadings);

        if (validReadings.Count > 0)
        {
            await _meterReadingRepository.AddAsync(validReadings);
        }

        return new MeterReadingUpdateResult
        {
            SuccessfulReadings = validReadings.Count,
            FailedReadings = meterReadingCsv.InvalidMeterReadings + invalidReadings.Count
        };
    }
}

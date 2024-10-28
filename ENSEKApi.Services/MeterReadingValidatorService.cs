using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using ENSEKApi.Services.Validators;

namespace ENSEKApi.Services;

public partial class MeterReadingValidatorService(IMeterReadingRepository meterReadingRepo, IAccountRepository accountRepository)
{
    private readonly IMeterReadingRepository _meterReadingRepository = meterReadingRepo;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task<(List<MeterReading> validReadings, List<MeterReading> invalidReadings)> ValidateAsync(IEnumerable<MeterReading> meterReadings)
    {
        var validReadings = new List<MeterReading>();
        var invalidReadings = new List<MeterReading>();
        var existingReadings = await _meterReadingRepository.GetAllAsync();
        var existingAccounts = await _accountRepository.GetAllAccountsAsync();
        var accountIds = existingAccounts.Select(x => x.AccountId);

        var validator = new MeterReadingValidator(existingReadings, accountIds);

        foreach (var reading in meterReadings)
        {
            var result = validator.Validate(reading);
            if (result.IsValid)
            {
                validReadings.Add(reading);
            }
            else
            {
                invalidReadings.Add(reading);
            }
        }

        return (validReadings, invalidReadings);
    }
}

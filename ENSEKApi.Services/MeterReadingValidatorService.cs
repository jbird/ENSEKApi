using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using System.Text.RegularExpressions;

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

        foreach (var reading in meterReadings)
        {
            if (IsValidReading(reading, existingReadings, existingAccounts.Select(x => x.AccountId)))
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

    private static bool IsValidReading(MeterReading reading, IEnumerable<MeterReading> existingReadings, IEnumerable<int> accountIds)
    {
        // Check if reading value is in the format NNNNN
        if (!ReadingValueRegex().IsMatch(reading.ReadValue.ToString()))
        {
            return false;
        }

        // Check if the same entry already exists
        if (existingReadings.Any(r => r.AccountId == reading.AccountId && r.ReadingDateTime == reading.ReadingDateTime))
        {
            return false;
        }

        // Check if reading is associated with a valid Account ID
        if (!accountIds.Contains(reading.AccountId))
        {
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"^\d{5}$")]
    private static partial Regex ReadingValueRegex();
}
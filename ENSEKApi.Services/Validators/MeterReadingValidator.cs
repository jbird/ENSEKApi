using ENSEKApi.Models;
using FluentValidation;

namespace ENSEKApi.Services.Validators;

public class MeterReadingValidator : AbstractValidator<MeterReading>
{
    public MeterReadingValidator(IEnumerable<MeterReading> existingReadings, IEnumerable<int> accountIds)
    {
        RuleFor(reading => reading.ReadValue.ToString())
            .Matches(@"^\d{5}$")
            .WithMessage("Reading value must be in the format NNNNN");

        RuleFor(reading => reading)
            .Must(reading => !existingReadings.Any(r => r.AccountId == reading.AccountId && r.ReadingDateTime == reading.ReadingDateTime))
            .WithMessage("Duplicate reading entry");

        RuleFor(reading => reading.AccountId)
            .Must(accountId => accountIds.Contains(accountId))
            .WithMessage("Invalid Account ID");

        RuleFor(reading => reading)
            .Must(reading =>
            {
                var existingReading = existingReadings
                    .Where(r => r.AccountId == reading.AccountId)
                    .OrderByDescending(r => r.ReadingDateTime)
                    .FirstOrDefault();

                return existingReading == null || reading.ReadingDateTime > existingReading.ReadingDateTime;
            })
            .WithMessage("New reading must be newer than the existing reading for the account");
    }
}

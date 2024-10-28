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
    }
}

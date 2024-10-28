using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using ENSEKApi.Services;
using Moq;

namespace ENSEKApi.Api.Services.Tests;

public class MeterReadingValidatorServiceTests
{
    private readonly Mock<IMeterReadingRepository> _meterReadingRepositoryMock;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly MeterReadingValidatorService _validatorService;

    public MeterReadingValidatorServiceTests()
    {
        _meterReadingRepositoryMock = new Mock<IMeterReadingRepository>();
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _validatorService = new MeterReadingValidatorService(_meterReadingRepositoryMock.Object, _accountRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnValidReadings_WhenReadingsAreValid()
    {
        // Arrange
        var meterReadings = new List<MeterReading>
            {
                new() { AccountId = 1, ReadingDateTime = DateTime.Now, ReadValue = 12345 }
            };

        var existingReadings = new List<MeterReading>();
        var existingAccounts = new List<Account> { new() { AccountId = 1, AccountName = "Test", AccountType = "Type" } };

        _meterReadingRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(existingReadings);
        _accountRepositoryMock.Setup(repo => repo.GetAllAccountsAsync()).ReturnsAsync(existingAccounts);

        // Act
        var (validReadings, invalidReadings) = await _validatorService.ValidateAsync(meterReadings);

        // Assert
        Assert.Single(validReadings);
        Assert.Empty(invalidReadings);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidReadings_WhenReadingsAreInvalid()
    {
        // Arrange
        var meterReadings = new List<MeterReading>
            {
                new() { AccountId = 1, ReadingDateTime = DateTime.Now, ReadValue = 123 }
            };

        var existingReadings = new List<MeterReading>();
        var existingAccounts = new List<Account> { new() { AccountId = 1, AccountName = "Test", AccountType = "Type" } };

        _meterReadingRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(existingReadings);
        _accountRepositoryMock.Setup(repo => repo.GetAllAccountsAsync()).ReturnsAsync(existingAccounts);

        // Act
        var (validReadings, invalidReadings) = await _validatorService.ValidateAsync(meterReadings);

        // Assert
        Assert.Empty(validReadings);
        Assert.Single(invalidReadings);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidReadings_WhenAccountIdIsInvalid()
    {
        // Arrange
        var meterReadings = new List<MeterReading>
            {
                new() { AccountId = 2, ReadingDateTime = DateTime.Now, ReadValue = 12345 }
            };

        var existingReadings = new List<MeterReading>();
        var existingAccounts = new List<Account> { new() { AccountId = 1, AccountName = "Test", AccountType = "Type" } };

        _meterReadingRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(existingReadings);
        _accountRepositoryMock.Setup(repo => repo.GetAllAccountsAsync()).ReturnsAsync(existingAccounts);

        // Act
        var (validReadings, invalidReadings) = await _validatorService.ValidateAsync(meterReadings);

        // Assert
        Assert.Empty(validReadings);
        Assert.Single(invalidReadings);
    }
}

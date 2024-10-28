using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using ENSEKApi.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ENSEKApi.Api.Services.Tests;

public class MeterReadingServiceTests
{
    private readonly Mock<ILogger<MeterReadingService>> _loggerMock;
    private readonly Mock<IMeterReadingRepository> _meterReadingRepositoryMock;
    private readonly Mock<IMeterReadingValidatorService> _validatorServiceMock;
    private readonly MeterReadingService _meterReadingService;

    public MeterReadingServiceTests()
    {
        _loggerMock = new Mock<ILogger<MeterReadingService>>();
        _meterReadingRepositoryMock = new Mock<IMeterReadingRepository>();
        _validatorServiceMock = new Mock<IMeterReadingValidatorService>();
        _meterReadingService = new MeterReadingService(_loggerMock.Object, _meterReadingRepositoryMock.Object, _validatorServiceMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddValidReadings()
    {
        // Arrange
        var validReadings = new List<MeterReading>
            {
                new() { AccountId = 1, ReadingDateTime = DateTime.Now, ReadValue = 12345 }
            };

        var invalidReadings = new List<MeterReading>();
        var meterReadingCsv = new MeterReadingCsv
        {
            ValidMeterReadings = validReadings,
            InvalidMeterReadings = 0
        };

        _validatorServiceMock.Setup(v => v.ValidateAsync(validReadings)).ReturnsAsync((validReadings, invalidReadings));

        // Act
        var result = await _meterReadingService.AddAsync(meterReadingCsv);

        // Assert
        _meterReadingRepositoryMock.Verify(r => r.AddAsync(validReadings), Times.Once);
        Assert.Equal(validReadings.Count, result.SuccessfulReadings);
        Assert.Equal(0, result.FailedReadings);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnCorrectFailedReadingsCount()
    {
        // Arrange
        var validReadings = new List<MeterReading>();
        var invalidReadings = new List<MeterReading>
            {
                new() { AccountId = 1, ReadingDateTime = DateTime.Now, ReadValue = 123 }
            };

        var meterReadingCsv = new MeterReadingCsv
        {
            ValidMeterReadings = validReadings,
            InvalidMeterReadings = 1
        };

        _validatorServiceMock.Setup(v => v.ValidateAsync(validReadings)).ReturnsAsync((validReadings, invalidReadings));

        // Act
        var result = await _meterReadingService.AddAsync(meterReadingCsv);

        // Assert
        Assert.Equal(0, result.SuccessfulReadings);
        Assert.Equal(2, result.FailedReadings); // 1 from meterReadingCsv.InvalidMeterReadings + 1 from invalidReadings.Count
    }
}
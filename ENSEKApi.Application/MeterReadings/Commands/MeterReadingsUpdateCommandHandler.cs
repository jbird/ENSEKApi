using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using ENSEKApi.Services.Helpers;
using MediatR;

namespace ENSEKApi.Application.MeterReadings.Commands;

public class MeterReadingsUpdateCommandHandler(IMeterReadingService meterReadingService) : IRequestHandler<MeterReadingsUpdateCommand, MeterReadingUpdateResult>
{
    private readonly IMeterReadingService _meterReadingService = meterReadingService;

    public async Task<MeterReadingUpdateResult> Handle(MeterReadingsUpdateCommand request, CancellationToken cancellationToken)
    {
        var csvMeterReadings = await MeterReadingHelper.ConvertCsvFileToMeterReadingsAsync(request.File);
        return await _meterReadingService.AddAsync(csvMeterReadings);
    }
}

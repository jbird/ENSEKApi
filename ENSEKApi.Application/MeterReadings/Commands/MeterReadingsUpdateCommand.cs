using ENSEKApi.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ENSEKApi.Application.MeterReadings.Commands;

public class MeterReadingsUpdateCommand(IFormFile file) : IRequest<MeterReadingUpdateResult>
{
    public IFormFile File { get; set; } = file;
}

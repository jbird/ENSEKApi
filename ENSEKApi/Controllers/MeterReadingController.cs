using ENSEKApi.Application.MeterReadings.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ENSEKApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeterReadingController(ILogger<MeterReadingController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<MeterReadingController> _logger = logger;
    private readonly IMediator _mediator = mediator;

    [HttpPost("meter-reading-uploads")]
    public async Task<IActionResult> UploadMeterReading(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            _logger.LogError("Invalid file.");
            return BadRequest("Invalid file.");
        }

        var result = await _mediator.Send(new MeterReadingsUpdateCommand(file));

        return Ok(new
        {
            Message = "Meter readings uploaded successfully.",
            result.SuccessfulReadings,
            result.FailedReadings
        });
    }
}
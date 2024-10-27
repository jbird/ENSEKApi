using ENSEKApi.Application.MeterReadings.Commands;
using ENSEKApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ENSEKApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeterReadingController : ControllerBase
{
    private readonly ILogger<MeterReadingController> _logger;
    private readonly IMediator _mediator;

    public MeterReadingController(ILogger<MeterReadingController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("meter-reading-uploads")]
    public async Task<IActionResult> UploadMeterReading(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid file.");
        }

        var command = new MeterReadingsUpdateCommand(file);
        var result = await _mediator.Send(command);

        return Ok(new
        {
            Message = "Meter readings uploaded successfully.",
            SuccessfulReadings = result.SuccessfulReadings,
            FailedReadings = result.FailedReadings
        });
    }
}
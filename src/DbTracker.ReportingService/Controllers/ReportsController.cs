namespace DbTracker.ReportingService.Controllers;

using DbTracker.ReportingService.Services;
using DbTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportingService _reportingService;

    public ReportsController(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    [HttpGet("critical")]
    public async Task<ActionResult<IEnumerable<AuditEvent>>> GetCriticalEvents(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var events = await _reportingService.GetCriticalEventsAsync(startDate, endDate);
        return Ok(events);
    }

    [HttpGet("by-type/{eventType}")]
    public async Task<ActionResult<IEnumerable<AuditEvent>>> GetEventsByType(
        string eventType,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var events = await _reportingService.GetEventsByTypeAsync(eventType, startDate, endDate);
        return Ok(events);
    }

    [HttpGet("by-user/{username}")]
    public async Task<ActionResult<IEnumerable<AuditEvent>>> GetEventsByUser(
        string username,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var events = await _reportingService.GetEventsByUserAsync(username, startDate, endDate);
        return Ok(events);
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<IDictionary<string, long>>> GetStatistics(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var stats = await _reportingService.GetEventTypeStatisticsAsync(startDate, endDate);
        return Ok(stats);
    }
} 
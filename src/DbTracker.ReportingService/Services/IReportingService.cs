namespace DbTracker.ReportingService.Services;

using DbTracker.Shared.Models;

public interface IReportingService
{
    Task<IEnumerable<AuditEvent>> GetCriticalEventsAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<AuditEvent>> GetEventsByTypeAsync(string eventType, DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<AuditEvent>> GetEventsByUserAsync(string username, DateTime? startDate = null, DateTime? endDate = null);
    Task<IDictionary<string, long>> GetEventTypeStatisticsAsync(DateTime? startDate = null, DateTime? endDate = null);
} 
namespace DbTracker.CollectorService.Services;

using DbTracker.Shared.Models;

public interface IEventProducerService
{
    Task ProduceAuditEventAsync(AuditEvent auditEvent);
} 
namespace DbTracker.ProcessorService.Services;

using DbTracker.Shared.Models;

public interface IElasticsearchService
{
    Task IndexAuditEventAsync(AuditEvent auditEvent);
    Task EnsureIndexExistsAsync();
} 
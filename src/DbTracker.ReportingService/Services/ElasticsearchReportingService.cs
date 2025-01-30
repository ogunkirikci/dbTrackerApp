namespace DbTracker.ReportingService.Services;

using DbTracker.Shared.Models;
using Nest;

public class ElasticsearchReportingService : IReportingService
{
    private readonly IElasticClient _client;
    private const string IndexName = "audit-events";

    public ElasticsearchReportingService(IConfiguration configuration)
    {
        var settings = new ConnectionSettings(new Uri(configuration["Elasticsearch:Url"]))
            .DefaultIndex(IndexName);
        _client = new ElasticClient(settings);
    }

    public async Task<IEnumerable<AuditEvent>> GetCriticalEventsAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var searchResponse = await _client.SearchAsync<AuditEvent>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(
                        m => m.Term(t => t.IsCritical, true),
                        startDate.HasValue ? m => m.DateRange(r => r.Field(f => f.Timestamp).GreaterThanOrEquals(startDate.Value)) : null,
                        endDate.HasValue ? m => m.DateRange(r => r.Field(f => f.Timestamp).LessThanOrEquals(endDate.Value)) : null
                    )
                )
            )
            .Size(100)
        );

        return searchResponse.Documents;
    }

    public async Task<IEnumerable<AuditEvent>> GetEventsByTypeAsync(string eventType, DateTime? startDate = null, DateTime? endDate = null)
    {
        var searchResponse = await _client.SearchAsync<AuditEvent>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(
                        m => m.Term(t => t.EventType, eventType),
                        startDate.HasValue ? m => m.DateRange(r => r.Field(f => f.Timestamp).GreaterThanOrEquals(startDate.Value)) : null,
                        endDate.HasValue ? m => m.DateRange(r => r.Field(f => f.Timestamp).LessThanOrEquals(endDate.Value)) : null
                    )
                )
            )
            .Size(100)
        );

        return searchResponse.Documents;
    }

    public async Task<IEnumerable<AuditEvent>> GetEventsByUserAsync(string username, DateTime? startDate = null, DateTime? endDate = null)
    {
        var searchResponse = await _client.SearchAsync<AuditEvent>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(
                        m => m.Term(t => t.Username, username),
                        startDate.HasValue ? m => m.DateRange(r => r.Field(f => f.Timestamp).GreaterThanOrEquals(startDate.Value)) : null,
                        endDate.HasValue ? m => m.DateRange(r => r.Field(f => f.Timestamp).LessThanOrEquals(endDate.Value)) : null
                    )
                )
            )
            .Size(100)
        );

        return searchResponse.Documents;
    }

    public async Task<IDictionary<string, long>> GetEventTypeStatisticsAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var searchResponse = await _client.SearchAsync<AuditEvent>(s => s
            .Aggregations(a => a
                .Terms("event_types", t => t
                    .Field(f => f.EventType)
                    .Size(20)
                )
            )
            .Size(0)
        );

        var buckets = searchResponse.Aggregations.Terms("event_types").Buckets;
        return buckets.ToDictionary(b => b.Key, b => b.DocCount ?? 0);
    }
} 
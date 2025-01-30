namespace DbTracker.ProcessorService.Services;

using DbTracker.Shared.Models;
using Nest;

public class ElasticsearchService : IElasticsearchService
{
    private readonly IElasticClient _client;
    private const string IndexName = "audit-events";

    public ElasticsearchService(IConfiguration configuration)
    {
        var settings = new ConnectionSettings(new Uri(configuration["Elasticsearch:Url"]))
            .DefaultIndex(IndexName);
        _client = new ElasticClient(settings);
    }

    public async Task EnsureIndexExistsAsync()
    {
        if (!(await _client.Indices.ExistsAsync(IndexName)).Exists)
        {
            await _client.Indices.CreateAsync(IndexName, c => c
                .Map<AuditEvent>(m => m
                    .AutoMap()
                    .Properties(p => p
                        .Date(d => d.Name(n => n.Timestamp))
                        .Keyword(k => k.Name(n => n.EventType))
                        .Keyword(k => k.Name(n => n.Username))
                        .Keyword(k => k.Name(n => n.DatabaseName))
                    )
                )
            );
        }
    }

    public async Task IndexAuditEventAsync(AuditEvent auditEvent)
    {
        await _client.IndexDocumentAsync(auditEvent);
    }
} 
namespace DbTracker.ProcessorService.Services;

using Confluent.Kafka;
using DbTracker.Shared.Models;
using System.Text.Json;

public class KafkaConsumerService : BackgroundService
{
    private readonly string _topic = "audit-events";
    private readonly IConsumer<string, string> _consumer;
    private readonly IElasticsearchService _elasticsearchService;
    private readonly ILogger<KafkaConsumerService> _logger;

    public KafkaConsumerService(
        IConfiguration configuration,
        IElasticsearchService elasticsearchService,
        ILogger<KafkaConsumerService> logger)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "processor-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _elasticsearchService = elasticsearchService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _elasticsearchService.EnsureIndexExistsAsync();
        
        _consumer.Subscribe(_topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);
                var auditEvent = JsonSerializer.Deserialize<AuditEvent>(result.Message.Value);

                if (auditEvent != null)
                {
                    await _elasticsearchService.IndexAuditEventAsync(auditEvent);
                    _logger.LogInformation("Processed and indexed event: {EventType}", auditEvent.EventType);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    public override void Dispose()
    {
        _consumer?.Dispose();
        base.Dispose();
    }
} 
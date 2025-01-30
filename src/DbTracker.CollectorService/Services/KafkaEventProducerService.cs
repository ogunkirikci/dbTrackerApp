namespace DbTracker.CollectorService.Services;

using Confluent.Kafka;
using DbTracker.Shared.Models;
using System.Text.Json;

public class KafkaEventProducerService : IEventProducerService, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private const string TopicName = "audit-events";

    public KafkaEventProducerService(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAuditEventAsync(AuditEvent auditEvent)
    {
        var message = new Message<string, string>
        {
            Key = auditEvent.EventId.ToString(),
            Value = JsonSerializer.Serialize(auditEvent)
        };

        await _producer.ProduceAsync(TopicName, message);
    }

    public void Dispose()
    {
        _producer?.Dispose();
    }
} 
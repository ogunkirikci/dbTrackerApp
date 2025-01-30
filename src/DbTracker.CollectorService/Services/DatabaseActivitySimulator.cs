namespace DbTracker.CollectorService.Services;

using DbTracker.Shared.Models;

public class DatabaseActivitySimulator : BackgroundService
{
    private readonly IEventProducerService _eventProducer;
    private readonly ILogger<DatabaseActivitySimulator> _logger;
    private readonly Random _random = new();

    public DatabaseActivitySimulator(
        IEventProducerService eventProducer,
        ILogger<DatabaseActivitySimulator> logger)
    {
        _eventProducer = eventProducer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var auditEvent = GenerateRandomAuditEvent();
                await _eventProducer.ProduceAuditEventAsync(auditEvent);
                _logger.LogInformation("Produced event: {EventType}", auditEvent.EventType);
                
                // Her 2-5 saniyede bir olay üret
                await Task.Delay(TimeSpan.FromSeconds(_random.Next(2, 6)), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error producing audit event");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

    private AuditEvent GenerateRandomAuditEvent()
    {
        var eventTypes = new[] { "SELECT", "INSERT", "UPDATE", "DELETE", "LOGIN", "LOGOUT" };
        var users = new[] { "admin", "user1", "user2", "system" };
        var databases = new[] { "CustomerDB", "OrderDB", "InventoryDB" };

        return new AuditEvent
        {
            EventId = Guid.NewGuid(),
            EventType = eventTypes[_random.Next(eventTypes.Length)],
            Timestamp = DateTime.UtcNow,
            Username = users[_random.Next(users.Length)],
            DatabaseName = databases[_random.Next(databases.Length)],
            Statement = GenerateRandomStatement(),
            Severity = _random.Next(2) == 0 ? EventSeverity.Normal : EventSeverity.High,
            IsCritical = _random.Next(2) == 1
        };
    }

    private string GenerateRandomStatement()
    {
        return "SELECT * FROM Users WHERE Id = 1"; // Basit örnek
    }
} 
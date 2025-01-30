namespace DbTracker.Shared.Models;

public class NotificationConfig
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public NotificationType Type { get; set; }
    public required string Configuration { get; set; } // JSON formatında email/SMS ayarları
    public bool IsActive { get; set; }
}

public enum NotificationType
{
    Email,
    SMS,
    Webhook
} 
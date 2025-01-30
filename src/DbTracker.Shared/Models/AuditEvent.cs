namespace DbTracker.Shared.Models;
using System.Text.Json.Serialization;

public class AuditEvent
{
    public Guid EventId { get; set; }
    public required string EventType { get; set; }
    public DateTime Timestamp { get; set; }
    public required string Username { get; set; }
    public required string DatabaseName { get; set; }
    public required string Statement { get; set; }
    public EventSeverity Severity { get; set; }
    public bool IsCritical { get; set; }
}

public enum EventSeverity
{
    [JsonPropertyName("Normal")]
    Normal = 0,
    
    [JsonPropertyName("High")]
    High = 1
}

public static class EventTypes
{
    public const string Select = "SELECT";
    public const string Insert = "INSERT";
    public const string Update = "UPDATE";
    public const string Delete = "DELETE";
    public const string Login = "LOGIN";
    public const string Logout = "LOGOUT";
} 
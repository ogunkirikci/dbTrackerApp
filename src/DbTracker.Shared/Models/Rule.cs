namespace DbTracker.Shared.Models;

public class Rule
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string EventType { get; set; }
    public bool IsCritical { get; set; }
    public EventSeverity MinimumSeverity { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
} 
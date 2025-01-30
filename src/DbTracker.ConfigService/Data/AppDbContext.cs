using DbTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTracker.ConfigService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Rule> Rules { get; set; }
    public DbSet<NotificationConfig> NotificationConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Rule entity konfigürasyonu
        modelBuilder.Entity<Rule>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Rule>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        // NotificationConfig entity konfigürasyonu
        modelBuilder.Entity<NotificationConfig>()
            .HasKey(n => n.Id);

        modelBuilder.Entity<NotificationConfig>()
            .Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
} 
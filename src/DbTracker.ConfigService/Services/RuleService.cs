namespace DbTracker.ConfigService.Services;

using DbTracker.ConfigService.Data;
using DbTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

public class RuleService : IRuleService
{
    private readonly AppDbContext _context;

    public RuleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rule>> GetAllRulesAsync()
    {
        return await _context.Rules.ToListAsync();
    }

    public async Task<Rule?> GetRuleByIdAsync(Guid id)
    {
        return await _context.Rules.FindAsync(id);
    }

    public async Task<Rule> CreateRuleAsync(Rule rule)
    {
        rule.Id = Guid.NewGuid();
        _context.Rules.Add(rule);
        await _context.SaveChangesAsync();
        return rule;
    }

    public async Task<Rule?> UpdateRuleAsync(Guid id, Rule rule)
    {
        var existingRule = await _context.Rules.FindAsync(id);
        if (existingRule == null) return null;

        existingRule.Name = rule.Name;
        existingRule.EventType = rule.EventType;
        existingRule.IsCritical = rule.IsCritical;
        existingRule.MinimumSeverity = rule.MinimumSeverity;
        existingRule.IsActive = rule.IsActive;
        existingRule.Description = rule.Description;

        await _context.SaveChangesAsync();
        return existingRule;
    }

    public async Task<bool> DeleteRuleAsync(Guid id)
    {
        var rule = await _context.Rules.FindAsync(id);
        if (rule == null) return false;

        _context.Rules.Remove(rule);
        await _context.SaveChangesAsync();
        return true;
    }
} 
namespace DbTracker.ConfigService.Services;

using DbTracker.Shared.Models;

public interface IRuleService
{
    Task<IEnumerable<Rule>> GetAllRulesAsync();
    Task<Rule?> GetRuleByIdAsync(Guid id);
    Task<Rule> CreateRuleAsync(Rule rule);
    Task<Rule?> UpdateRuleAsync(Guid id, Rule rule);
    Task<bool> DeleteRuleAsync(Guid id);
} 
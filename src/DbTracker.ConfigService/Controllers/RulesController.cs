namespace DbTracker.ConfigService.Controllers;

using DbTracker.ConfigService.Services;
using DbTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RulesController : ControllerBase
{
    private readonly IRuleService _ruleService;

    public RulesController(IRuleService ruleService)
    {
        _ruleService = ruleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rule>>> GetRules()
    {
        var rules = await _ruleService.GetAllRulesAsync();
        return Ok(rules);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rule>> GetRule(Guid id)
    {
        var rule = await _ruleService.GetRuleByIdAsync(id);
        if (rule == null) return NotFound();
        return Ok(rule);
    }

    [HttpPost]
    public async Task<ActionResult<Rule>> CreateRule([FromBody] Rule rule)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdRule = await _ruleService.CreateRuleAsync(rule);
        return CreatedAtAction(nameof(GetRule), new { id = createdRule.Id }, createdRule);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRule(Guid id, [FromBody] Rule rule)
    {
        var updatedRule = await _ruleService.UpdateRuleAsync(id, rule);
        if (updatedRule == null) return NotFound();
        return Ok(updatedRule);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRule(Guid id)
    {
        var result = await _ruleService.DeleteRuleAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
} 
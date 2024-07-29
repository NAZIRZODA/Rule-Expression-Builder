namespace RuleExpressionBuilder.Models;

public class RuleEntity
{
    public string TargetDoamainType { get; set; } = null!;
    public RuleClause[]? Clauses { get; set; }
    public RuleClauseGroup[]? ClauseGroups { get; set; }
}
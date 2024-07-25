namespace ExpressionBuilder.Models;

public class RuleEntity
{
    public RuleClause[]? Clauses { get; set; }
    public RuleClauseGroup[]? ClauseGroups { get; set; }
}
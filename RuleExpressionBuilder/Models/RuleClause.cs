using RuleExpressionBuilder.Enums;

namespace RuleExpressionBuilder.Models;

public class RuleClause
{
    public LogicalOperatorType LogicalOperator { get; set; }
    public string FieldName { get; set; } = null!;
    public OperatorType Operator { get; set; }
    public string Value { get; set; } = null!;
    public int Index { get; set; }
}
using System.Text;
using ExpressionBuilder.Enums;
using ExpressionBuilder.Models;

namespace ExpressionBuilder.Services;

public class RuleParser : IRuleParser
{
    public string BuildExpression(RuleEntity ruleEntity)
    {
        if (ruleEntity.Clauses == null || ruleEntity.Clauses.Length == 0)
        {
            return string.Empty;
        }

        var clauses = ruleEntity.Clauses
            .OrderBy(c => c.Index)
            .ToList();
        
        var clauseGroups = ruleEntity.ClauseGroups?
                               .OrderBy(g => g.Level)
                               .ThenBy(g => g.Start)
                               .ToList()
                           ?? new List<RuleClauseGroup>();

        var expression = new StringBuilder();
        var groupStack = new Stack<int>();
        bool isFirstClause = true;

        foreach (var clause in clauses)
        {
            foreach (var group in clauseGroups.Where(g => g.Start == clause.Index))
            {
                if (!isFirstClause && !expression.ToString().EndsWith("("))
                {
                    expression.Append($" {clause.LogicalOperator.ToString().ToUpper()} ");
                }

                expression.Append("(");
                groupStack.Push(group.Level);
            }
            
            if (!isFirstClause && !expression.ToString().EndsWith("("))
            {
                expression.Append($" {clause.LogicalOperator.ToString().ToUpper()} ");
            }
            
            expression.Append(BuildClauseExpression(clause));
            isFirstClause = false;
            
            foreach (var group in clauseGroups.Where(g => g.End == clause.Index))
            {
                expression.Append(")");
                groupStack.Pop();
            }
        }

        return expression.ToString();
    }

    private static string BuildClauseExpression(RuleClause clause)
    {
        var operatorString = GetOperatorString(clause.Operator, clause.Value);
        return $"{clause.FieldName} {operatorString} \"{clause.Value}\"";
    }

    private static string GetOperatorString(OperatorType operatorType, string value)
    {
        return operatorType switch
        {
            OperatorType.Equals => "=",
            OperatorType.NotEquals => "!=",
            OperatorType.GreaterThan => ">",
            OperatorType.LessThan => "<",
            OperatorType.GreaterThanOrEqual => ">=",
            OperatorType.LessThanOrEqual => "<=",
            OperatorType.Empty => "IS EMPTY",
            OperatorType.NotEmpty => "IS NOT EMPTY",
            OperatorType.Contains => $"Contains({value})",
            _ => throw new NotImplementedException($"Operator {operatorType} is not implemented."),
        };
    }
}
using ExpressionBuilder.Models;

namespace ExpressionBuilder.Services;

public interface IRuleParser
{
    string BuildExpression(RuleEntity ruleEntity);

}
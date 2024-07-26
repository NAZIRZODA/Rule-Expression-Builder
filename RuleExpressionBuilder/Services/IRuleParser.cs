using RuleExpressionBuilder.Models;

namespace RuleExpressionBuilder.Services;

public interface IRuleParser
{
    string BuildExpression(RuleEntity ruleEntity);

}
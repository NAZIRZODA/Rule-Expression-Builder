using System.Text;
using RuleExpressionBuilder.Enums;
using RuleExpressionBuilder.Models;

namespace RuleExpressionBuilder.Services;

public class RuleParser : IRuleParser
{
    private readonly Dictionary<string, DomainObjectEntity> _domainObjects;

    public RuleParser()
    {
        _domainObjects = new()
        {
            {
                "Order", new DomainObjectEntity()
                {
                    Type = "Order",
                    Properties = new List<DomainObjectEntity.PropertyUnit>()
                    {
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "BusinessUnitId",
                            PropertyType = typeof(string).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "OrderStatus",
                            PropertyType = typeof(int).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "IsActive",
                            PropertyType = typeof(bool).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "CreatedDate",
                            PropertyType = typeof(DateTime).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "CustomerCount",
                            PropertyType = typeof(int).FullName!
                        }
                    }
                }
            },
            {
                "User", new DomainObjectEntity()
                {
                    Type = "User",
                    Properties = new List<DomainObjectEntity.PropertyUnit>()
                    {
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Country",
                            PropertyType = typeof(string).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Age",
                            PropertyType = typeof(int).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "SubscriptionStatus",
                            PropertyType = typeof(string).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "LastLoginDate",
                            PropertyType = typeof(DateTime).FullName!
                        }
                    }
                }
            },
            {
                "OrderLine", new DomainObjectEntity()
                {
                    Type = "OrderLine",
                    Properties = new List<DomainObjectEntity.PropertyUnit>()
                    {
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "ProductCategory",
                            PropertyType = typeof(string).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Price",
                            PropertyType = typeof(int).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Brand",
                            PropertyType = typeof(string).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Stock",
                            PropertyType = typeof(int).FullName!
                        }
                    }
                }
            },
            {
                "Worker", new DomainObjectEntity()
                {
                    Type = "Worker",
                    Properties = new List<DomainObjectEntity.PropertyUnit>()
                    {
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Department",
                            PropertyType = typeof(string).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Salary",
                            PropertyType = typeof(int).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "Location",
                            PropertyType = typeof(string).FullName!
                        },
                        new DomainObjectEntity.PropertyUnit()
                        {
                            PropertyName = "EmploymentDate",
                            PropertyType = typeof(DateTime).FullName!
                        }
                    }
                }
            }
        };
    }

    public string BuildExpression(RuleEntity ruleEntity)
    {
        if (ruleEntity.Clauses == null || ruleEntity.Clauses.Length == 0)
        {
            return string.Empty;
        }

        var clauses = ruleEntity.Clauses
            .OrderBy(c => c.Index)
            .ToList();

        var domainObject = _domainObjects[ruleEntity.TargetDoamainType];

        var expressionBuilder = new StringBuilder();

        foreach (var clause in clauses)
        {
            var openParenthesisCount = ruleEntity.ClauseGroups?.Count(g => g.Start == clause.Index) ?? 0;
            var closeParenthesisCount = ruleEntity.ClauseGroups?.Count(g => g.End == clause.Index) ?? 0;

            var propertyUnit = domainObject.Properties.First(p => p.PropertyName == clause.FieldName);
            var clauseStr = BuildClauseExpression(clause, propertyUnit);

            if (expressionBuilder.Length > 0)
            {
                expressionBuilder.Append(clause.LogicalOperator.ToString());
                expressionBuilder.Append(' ');
            }

            if (openParenthesisCount > 0)
            {
                expressionBuilder.Append(new string('(', openParenthesisCount));
            }

            expressionBuilder.Append(clauseStr);

            if (closeParenthesisCount > 0)
            {
                expressionBuilder.Append(new string(')', closeParenthesisCount));
            }

            expressionBuilder.Append(' ');
        }

        return expressionBuilder.ToString();
    }

    private static string BuildClauseExpression(RuleClause clause, DomainObjectEntity.PropertyUnit propertyUnit)
    {
        var type = Type.GetType(propertyUnit.PropertyType)!;
        
        var value = $"{clause.FieldName}_ExpectedValue{clause.Index}";
        return clause.Operator switch
        {
            OperatorType.IsNull => GetBinaryExpression(clause.FieldName, "null", "==", type),
            OperatorType.IsNotNull => GetBinaryExpression(clause.FieldName, "null", "!=", type),
            OperatorType.Equal => GetBinaryExpression(clause.FieldName, value, "==", type),
            OperatorType.NotEqual => GetBinaryExpression(clause.FieldName, value, "!=", type),
            OperatorType.GreaterThan => GetBinaryExpression(clause.FieldName, value, ">", type),
            OperatorType.LessThan => GetBinaryExpression(clause.FieldName, value, "<", type),
            OperatorType.GreaterThanOrEqual =>GetBinaryExpression(clause.FieldName, value, ">=", type),
            OperatorType.LessThanOrEqual => GetBinaryExpression(clause.FieldName, value, "<=", type),
            OperatorType.IsTrue => GetBinaryExpression(clause.FieldName, "true", "==", type),
            OperatorType.IsFalse => GetBinaryExpression(clause.FieldName, "false", "==", type),
            OperatorType.Empty => $"{clause.FieldName} == null || {clause.FieldName}.Length == 0",
            OperatorType.NotEmpty => $"{clause.FieldName} != null && {clause.FieldName}.Length > 0",
            OperatorType.Contains => $"{clause.FieldName} != null && {clause.FieldName}.Contains({value})",
            OperatorType.NotContains =>  $"{clause.FieldName} == null && {clause.FieldName}.Contains({value}) == false",
            OperatorType.In => $"{value}.Contains({clause.FieldName})",
            OperatorType.NotIn => $"{value}.Contains({clause.FieldName}) == false",
            _ => throw new NotImplementedException($"Operator {clause.Operator} is not implemented."),
        };
    }

    private static string GetBinaryExpression(string fieldName, string value, string operatorType, Type propertyType)
    {
        return $"{fieldName} {operatorType} {value}";
    }
}
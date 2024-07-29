using RuleExpressionBuilder.Enums;
using RuleExpressionBuilder.Models;
using RuleExpressionBuilder.Services;

var list = new List<RuleEntity>()
{
    new RuleEntity()
    {
        TargetDoamainType = "Order",
        Clauses =
        [
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "BusinessUnitId",
                Operator = OperatorType.Equal,
                Value = "SpecificBUId",
                Index = 1
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "OrderStatus",
                Operator = OperatorType.Equal,
                Value = "7",
                Index = 2
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "OrderStatus",
                Operator = OperatorType.Equal,
                Value = "8",
                Index = 3
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "IsActive",
                Operator = OperatorType.Equal,
                Value = "true",
                Index = 4
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "CreatedDate",
                Operator = OperatorType.GreaterThan,
                Value = "2023-01-01",
                Index = 5
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "CustomerCount",
                Operator = OperatorType.GreaterThanOrEqual,
                Value = "100",
                Index = 6
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "CustomerCount",
                Operator = OperatorType.LessThan,
                Value = "50",
                Index = 7
            }
        ],
        ClauseGroups = new[]
        {
            new RuleClauseGroup
            {
                Start = 1,
                End = 3,
                Level = 1
            },
            new RuleClauseGroup
            {
                Start = 6,
                End = 7,
                Level = 1
            },
            new RuleClauseGroup
            {
                Start = 1,
                End = 7,
                Level = 0
            }
        }
    },
    new RuleEntity()
    {
        TargetDoamainType = "User",
        Clauses = new[]
        {
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Country",
                Operator = OperatorType.Equal,
                Value = "USA",
                Index = 1
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Age",
                Operator = OperatorType.GreaterThan,
                Value = "21",
                Index = 2
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "Age",
                Operator = OperatorType.LessThan,
                Value = "18",
                Index = 3
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "SubscriptionStatus",
                Operator = OperatorType.Equal,
                Value = "active",
                Index = 4
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "SubscriptionStatus",
                Operator = OperatorType.Equal,
                Value = "pending",
                Index = 5
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "LastLoginDate",
                Operator = OperatorType.GreaterThanOrEqual,
                Value = "2023-01-01",
                Index = 6
            }
        },
        ClauseGroups = new[]
        {
            new RuleClauseGroup
            {
                Start = 2,
                End = 3,
                Level = 1
            },
            new RuleClauseGroup
            {
                Start = 4,
                End = 5,
                Level = 1
            },
            new RuleClauseGroup
            {
                Start = 1,
                End = 6,
                Level = 0
            }
        }
    },
    new RuleEntity
    {
        TargetDoamainType = "OrderLine",
        Clauses = new[]
        {
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "ProductCategory",
                Operator = OperatorType.Equal,
                Value = "Electronics",
                Index = 1
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "Price",
                Operator = OperatorType.GreaterThan,
                Value = "1000",
                Index = 2
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Price",
                Operator = OperatorType.LessThan,
                Value = "2000",
                Index = 3
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "Brand",
                Operator = OperatorType.Equal,
                Value = "Sony",
                Index = 4
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Brand",
                Operator = OperatorType.Equal,
                Value = "Samsung",
                Index = 5
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Stock",
                Operator = OperatorType.GreaterThanOrEqual,
                Value = "10",
                Index = 6
            }
        },
        ClauseGroups = new[]
        {
            new RuleClauseGroup
            {
                Start = 2,
                End = 3,
                Level = 1
            },
            new RuleClauseGroup
            {
                Start = 4,
                End = 5,
                Level = 1
            },
            new RuleClauseGroup
            {
                Start = 1,
                End = 6,
                Level = 0
            }
        }
    },
    new RuleEntity
    {
        TargetDoamainType = "Worker",
        Clauses = new[]
        {
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Department",
                Operator = OperatorType.Equal,
                Value = "HR",
                Index = 1
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "Salary",
                Operator = OperatorType.GreaterThan,
                Value = "50000",
                Index = 2
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Salary",
                Operator = OperatorType.LessThan,
                Value = "70000",
                Index = 3
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.Or,
                FieldName = "Location",
                Operator = OperatorType.Equal,
                Value = "New York",
                Index = 4
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "Location",
                Operator = OperatorType.Equal,
                Value = "San Francisco",
                Index = 5
            },
            new RuleClause
            {
                LogicalOperator = LogicalOperatorType.And,
                FieldName = "EmploymentDate",
                Operator = OperatorType.Contains,
                Value = "2022-01-01",
                Index = 6
            }
        },
        ClauseGroups = new[]
        {
            new RuleClauseGroup
            {
                Start = 2,
                End = 3,
                Level = 1
            },
            new RuleClauseGroup
            {
                Start = 4,
                End = 5,
                Level = 2
            },
            new RuleClauseGroup
            {
                Start = 1,
                End = 6,
                Level = 0
            }
        }
    }
};

IRuleParser ruleParser = new RuleParser();
foreach (var item in list)
{
    Console.WriteLine(ruleParser.BuildExpression(item));
}

Console.WriteLine();
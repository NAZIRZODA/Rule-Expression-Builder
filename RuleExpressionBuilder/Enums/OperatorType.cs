namespace RuleExpressionBuilder.Enums;

public enum OperatorType
{
    Equal,
    NotEqual,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
    Empty,
    NotEmpty,
    Contains,
    NotContains,
    IsNull,
    IsNotNull,
    In,
    NotIn,
    IsTrue,
    IsFalse
}

public class DomainObjectEntity
{
    public string Type { get; set; } = null!;

    public List<PropertyUnit> Properties { get; set; } = [];

    public class PropertyUnit
    {
        public string PropertyName { get; set; } = string.Empty;

        public string PropertyType { get; set; } = string.Empty;

        public bool IsCollection { get; set; }

        public OperatorType[]? AllowedOperators { get; set; }

        public PropertyAcceptableValue[]? AcceptableValues { get; set; }
    }

    public class PropertyAcceptableValue
    {
        public string Label { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
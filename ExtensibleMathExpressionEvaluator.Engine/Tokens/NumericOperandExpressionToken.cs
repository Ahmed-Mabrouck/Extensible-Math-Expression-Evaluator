using ExtensibleMathExpressionEvaluator.Contracts;

namespace ExtensibleMathExpressionEvaluator.Engine.Tokens
{
    /// <summary>
    /// Numeric Operand Expression Token Representing Numbers wuthin Any Expression.
    /// </summary>
    public class NumericOperandExpressionToken : IExpressionToken
    {
        public string Value { get; set; }

        public NumericOperandExpressionToken(string value)
        {
            Value = value;
        }

    }
}

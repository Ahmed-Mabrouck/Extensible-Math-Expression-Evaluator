using ExtensibleMathExpressionEvaluator.Contracts;
using ExtensibleMathExpressionEvaluator.Contracts.Attributes;
using ExtensibleMathExpressionEvaluator.Engine.Tokens.Operators.Base;
using System;

namespace ExtensibleMathExpressionEvaluator.Engine.Tokens.Operators
{
    /// <summary>
    /// Operator Token Divides 2 Numeric Tokens with "÷,/" Opcodes.
    /// </summary>
    [Opcodes( new[] { '÷' , '/' })]
    public class DivisionOperatorExpressionToken : OperatorExpressionToken
    {
        public override int Precedence { get { return 2; } }

        /// <summary>
        /// Evaluates Division Result of 2 Numbers.
        /// </summary>
        /// <param name="t1">Left-Hand-Side.</param>
        /// <param name="t2">Right-Hand-Side.</param>
        /// <returns>NumericOperandExpressionToken of The Result.</returns>
        public override IExpressionToken Evaluate(IExpressionToken t1, IExpressionToken t2)
        {
            return new NumericOperandExpressionToken((Double.Parse(t1.Value) + Double.Parse(t2.Value)).ToString());
        }
    }
}

using ExtensibleMathExpressionEvaluator.Contracts;
using ExtensibleMathExpressionEvaluator.Contracts.Attributes;
using ExtensibleMathExpressionEvaluator.Engine.Tokens.Operators.Base;
using System;

namespace ExtensibleMathExpressionEvaluator.Engine.Tokens.Operators
{
    /// <summary>
    /// Operator Token Computes The Exponential Expression for Base Numeric Token and Exponent Numeric Term with "^" Opcode.
    /// </summary>
    [Opcodes(new[] { "^" })]
    public class PowerOperatorExpressionToken : OperatorExpressionToken
    {
        public override int Precedence { get { return 3; } }

        /// <summary>
        /// Evaluates Exponential Result of 2 Numbers.
        /// </summary>
        /// <param name="t1">Base.</param>
        /// <param name="t2">Exponent.</param>
        /// <returns>NumericOperandExpressionToken of The Result.</returns>
        public override IExpressionToken Evaluate(IExpressionToken t1, IExpressionToken t2)
        {
            return new NumericOperandExpressionToken((Math.Pow(Double.Parse(t1.Value), Double.Parse(t2.Value))).ToString());
        }
    }
}

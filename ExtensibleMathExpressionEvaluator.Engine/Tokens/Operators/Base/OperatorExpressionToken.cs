using ExtensibleMathExpressionEvaluator.Contracts;
using ExtensibleMathExpressionEvaluator.Contracts.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ExtensibleMathExpressionEvaluator.Engine.Tokens.Operators.Base
{
    /// <summary>
    /// Template Class for Operator Expression Tokens Sets:-
    ///     - Value: The Default The First Opcode from <see cref="Contracts.Attributes.OpcodesAttribute"/>.
    ///     - Precedence: Default Value as -1.
    /// </summary>
    public abstract class OperatorExpressionToken : IOperatorExpressionToken
    {
        public string Value { get; set; }
        public virtual int Precedence { get => -1; }

        public OperatorExpressionToken()
        {
            var operatorsAttribute = GetType().GetCustomAttributes<OpcodesAttribute>()
                .FirstOrDefault();

            if (operatorsAttribute == null)
                throw new InvalidOperationException($"{GetType().FullName} should be decorated with Operators attribute to specify the operator token.");

            Value = operatorsAttribute.Opcodes
                .FirstOrDefault().ToString();

            if (String.IsNullOrEmpty(Value))
                throw new InvalidOperationException($"{GetType().FullName} empty operator token is not allowed.");
        }

        public abstract IExpressionToken Evaluate(IExpressionToken t1, IExpressionToken t2);
    }
}

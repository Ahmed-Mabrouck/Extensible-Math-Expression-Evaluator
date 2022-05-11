namespace ExtensibleMathExpressionEvaluator.Contracts
{
    /// <summary>
    /// The Base Contract for Operator Expression Token.
    /// You Can Extend The Engine By Implementing This Interface to Create New Operators.
    /// Implementing Types Should Be Decorated by <see cref="Attributes.OpcodesAttribute"/> to Map The Operator to The Expression Token Opcodes.
    /// </summary>
    public interface IOperatorExpressionToken : IExpressionToken
    {
        /// <summary>
        /// Operator Precedence Determines How Operators Are Parsed Concerning Each Other. Operators with Higher Precedence 
        /// Become The Operands of Operators with Lower Precedence.
        /// </summary>
        public int Precedence { get; }

        /// <summary>
        /// Evaluate Operator Expression on Both t1 and t2 Expression Tokens.
        /// </summary>
        /// <param name="t1">Left-Hand-Side Expression Token.</param>
        /// <param name="t2">Right-Hand-Side Expression Token.</param>
        /// <returns></returns>
        IExpressionToken Evaluate(IExpressionToken t1, IExpressionToken t2);
    }
}

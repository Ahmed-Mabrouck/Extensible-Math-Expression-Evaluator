using System;

namespace ExtensibleMathExpressionEvaluator.Engine.Exceptions
{
    /// <summary>
    /// Thrown If Tokenizing an Expression Failed Due to Syntax Errors.
    /// </summary>
    public class ExpressionSyntaxErrorException : InvalidOperationException
    {
        public ExpressionSyntaxErrorException(char c) : base($"Unexpected character {c}, Please check your expression and try again.") { }
    }
}

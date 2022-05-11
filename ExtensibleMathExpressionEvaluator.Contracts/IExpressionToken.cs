namespace ExtensibleMathExpressionEvaluator.Contracts
{
    /// <summary>
    /// The Base Contract for Expression Token.
    /// You Can Extend The Engine By Implementing This Interface to Create New Token Types.
    /// </summary>
    public interface IExpressionToken
    {
        /// <summary>
        /// The Value of The Token.
        /// </summary>
        string Value { get; set; }
    }
}

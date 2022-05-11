using System;

namespace ExtensibleMathExpressionEvaluator.Contracts.Attributes
{
    /// <summary>
    /// Used to Extend The Operator Tokens Capabilities by Decorating Them With Multiple Opcodes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class OpcodesAttribute : Attribute
    {
        public char[] Opcodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="op">Operator.</param>
        public OpcodesAttribute(char[] opcodes)
        {
            Opcodes = opcodes;
        }
    }
}

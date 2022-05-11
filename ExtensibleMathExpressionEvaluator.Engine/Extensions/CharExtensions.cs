using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensibleMathExpressionEvaluator.Engine.Extensions
{
    public static class CharExtensions
    {
        public static bool IsDigitOrFloatingPoint(this char c)
        {
            return Char.IsDigit(c) || c.Equals('.');
        }
    }
}

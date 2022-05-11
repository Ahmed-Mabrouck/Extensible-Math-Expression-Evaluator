using NUnit.Framework;
using ExtensibleMathExpressionEvaluator.Contracts;
using ExtensibleMathExpressionEvaluator.Contracts.Attributes;
using ExtensibleMathExpressionEvaluator.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExtensibleMathExpressionEvaluator.Tests
{
    public class Tests
    {
        /// <summary>
        /// Supported Operators Dictionary.
        /// </summary>
        public IDictionary<char, IOperatorExpressionToken> SupportedOperators { get; set; }

        /// <summary>
        /// Populate Operators Dictionary by Getting All Types in ExtensibleMathExpressionEvaluator.Engine Assembly That 
        /// Implements IOperatorExpressionToken and Decorated with Operators Attribute.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SupportedOperators = new Dictionary<char, IOperatorExpressionToken>();

            // Iterating All Types Implementing IOperatorExpressionToken and Decorated with Operators Attribute.
            foreach (var t in Assembly.GetAssembly(typeof(Expression))
                 .GetTypes()
                 .Where(t => t.IsDefined(typeof(OpcodesAttribute))
                    && t.GetInterfaces().Contains(typeof(IOperatorExpressionToken))))
            {
                // Create Instance of Each Type.
                var operatorExpressionToken = (IOperatorExpressionToken)Activator.CreateInstance(t);

                // Iterating All Assigned Operators (I.E. Multiplication Has Multiple Operator Opcodes */×).
                foreach (var op in t.GetCustomAttributes<OpcodesAttribute>().Single().Opcodes)
                {
                    // Add The Instansiated Object to The Operators Dictionary with Key Equals to Operator Opcodes.
                    SupportedOperators.Add(op, operatorExpressionToken);
                }
            }

            Assert.IsTrue(SupportedOperators.Count > 0);
        }

        [Test]
        public void TestMathematicalExpression()
        {
            double result = (3.5 + 5.5) * (4.5 - 2.5);
            Assert.AreEqual(result, Double.Parse(new Expression("(3.5 + 5.5) * (4.5 - 2.5)", SupportedOperators)
                .Parse()
                .Evaluate()
                .Value));
        }

    }
}
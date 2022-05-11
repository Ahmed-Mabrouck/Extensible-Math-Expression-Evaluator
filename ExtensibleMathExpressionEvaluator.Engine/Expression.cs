using ExtensibleMathExpressionEvaluator.Contracts;
using ExtensibleMathExpressionEvaluator.Engine.Exceptions;
using ExtensibleMathExpressionEvaluator.Engine.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using ExtensibleMathExpressionEvaluator.Engine.Extensions;
using System.Text;

namespace ExtensibleMathExpressionEvaluator.Engine
{
    /// <summary>
    /// Expression Model and Evaluation Implementation.
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// The Text Value of The Expression (Infix Notation).
        /// </summary>
        public string ExpressionText { get; private set; }
        /// <summary>
        /// Transformed Text Value of The Expression into Postfix Notation.
        /// </summary>
        public string PostfixExpressionText { get; private set; }
        /// <summary>
        /// Supported Operators Mapped by Their Opcodes.
        /// </summary>
        public IDictionary<string, IOperatorExpressionToken> SupportedOperators { get; private set; }
        /// <summary>
        /// Tokenized Expression in Postfix Notation.
        /// </summary>
        public Queue<IExpressionToken> PostfixExpressionTokens { get; private set; }
        /// <summary>
        /// The Evaluation Result of The Expression.
        /// </summary>
        public IExpressionToken Result { get; private set; }
        /// <summary>
        /// A Flag Holds Expression Tokenization Status.
        /// </summary>
        public bool IsTokenized { get => PostfixExpressionTokens != null; }
        /// <summary>
        /// A Flag Holds Expression Evaluation Status.
        /// </summary>
        public bool IsEvaluated { get => Result != null; }

        public Expression(string expressionText, IDictionary<String, IOperatorExpressionToken> supportedOperators)
        {
            // Remove Spaces from The Expression.
            ExpressionText = expressionText.Replace(" ", "");
            SupportedOperators = supportedOperators;
        }

        /// <summary>
        /// Parses The Expression by Tokenizing and Transforming It from Infix Notation into Postfix Notation.
        /// </summary>
        /// <returns>The Expression Object After Tokenization.</returns>
        public Expression Parse()
        {
            Tokenize();
            return this;
        }

        /// <summary>
        /// Evaluates The Expression Value.
        /// </summary>
        /// <returns></returns>
        public IExpressionToken Evaluate()
        {
            // Define Stack to Hold Operands Tokens, Operation Results and Finally Will Contain The Result.
            var stack = new Stack<IExpressionToken>();

            // Iterating The Queue to Dequeue The Expression Tokens.
            while (PostfixExpressionTokens.Count > 0)
            {
                var token = PostfixExpressionTokens.Dequeue();

                // Operator Tokens Behavior.
                if (token.GetType().GetInterfaces().Contains(typeof(IOperatorExpressionToken)))
                {
                    // Pop Operands Tokens from The Stack.
                    var t2 = stack.Pop();
                    var t1 = stack.Pop();

                    // Push Operation Result Token to The Stack.
                    stack.Push(SupportedOperators[token.Value].Evaluate(t1, t2));
                }
                // Operand Token Behavior.
                else
                {
                    // Push Operand Token to The Stack.
                    stack.Push(token);
                }
            }

            // Set The Final Evaluated Expression Result Token by Poping It from The Stack and Return It Back.
            Result = stack.Pop();
            return Result;
        }

        /// <summary>
        /// Transforms Expression from Infix Notation into Postfix Notation and Tokenize It in a Queue.
        /// </summary>
        private void Tokenize()
        {
            PostfixExpressionTokens = new Queue<IExpressionToken>();

            // Operators Stack Used to Transform Expression from Infix Notation into Postfix Notation.
            var operatorsStack = new Stack<String>();
            // String Buffer to Accumulate Operands Characters.
            var operandsBuilder = new StringBuilder();

            // Iterating Expression Text Characters.
            for (int i = 0; i < ExpressionText.Length; ++i)
            {
                char c = ExpressionText[i];

                // Digit or Floating Point Character Behavior.
                if (c.IsDigitOrFloatingPoint())
                {
                    operandsBuilder.Append(c);

                    // Iterate Expression String to Extract Number Consequent Characters.
                    while (++i <= (ExpressionText.Length - 1))
                    {
                        // Decrement Iterator Coutner Varibale and Break If The Character Is Not a Digit or a Floating Point.
                        if (!ExpressionText[i].IsDigitOrFloatingPoint())
                        {
                            --i;
                            break;
                        }

                        // Append The Character to Operand Builder As It Is a Digit or a Floating Point
                        operandsBuilder.Append(ExpressionText[i]);
                    }

                    // Enqueue Operand Token to Expression Tokens and Clear The Operands Accumulation Buffer.
                    PostfixExpressionTokens.Enqueue(new NumericOperandExpressionToken(operandsBuilder.ToString()));
                    operandsBuilder.Clear();
                }
                // Push Opening Parenthes '(' to Operators Stack to Override Operators Default Precedence.
                else if (c == '(')
                {
                    operatorsStack.Push(c.ToString());
                }
                // Tokenizing Operators Between Parentheses '(' and ')'.
                else if (c == ')')
                {
                    // Enqueue All The Operators Between the Parentheses '(' and ')' into The Expression Tokens Queue.
                    while (operatorsStack.Count > 0 &&
                        operatorsStack.Peek() != "(")
                    {
                        // Enqueues The Mapped Operator Token Object by Its Opcode from SupportedOperators Dictionary.
                        PostfixExpressionTokens.Enqueue(SupportedOperators[operatorsStack.Pop()]);
                    }

                    // Invalid Expression Syntax: Invalid Parenthes Locations.
                    if (operatorsStack.Count > 0 && operatorsStack.Peek() != "(")
                    {
                        throw new ExpressionSyntaxErrorException(c);
                    }
                    // Pop Opening Parenthes '(' from Operators Stack.
                    else
                    {
                        operatorsStack.Pop();
                    }
                }
                // Tokenizing Operators According to Their Precedence.
                else if (SupportedOperators.Keys.Contains(c.ToString()))
                {
                    // Tokenize All Previous Operators with Higher Precedence.
                    while (operatorsStack.Count > 0 && SupportedOperators.ContainsKey(operatorsStack.Peek())
                        && SupportedOperators[c.ToString()].Precedence <= SupportedOperators[operatorsStack.Peek()].Precedence)
                    {
                        PostfixExpressionTokens.Enqueue(SupportedOperators[operatorsStack.Pop()]);
                    }
                    // Push The Current Operator to Operators Stack.
                    operatorsStack.Push(c.ToString());
                }
                // Invalid Expression Syntax: Unexpected Character.
                else
                {
                    throw new ExpressionSyntaxErrorException(c);
                }
            }

            // Tokenize All Remaining Operators (Lower Precedence).
            while (operatorsStack.Count > 0)
            {
                PostfixExpressionTokens.Enqueue(SupportedOperators[operatorsStack.Pop()]);
            }

            // Formulate Postfix Expresion Text from Tokens for Better Readability.
            PostfixExpressionText = String.Join("", PostfixExpressionTokens.Select(t => t.Value));
        }
    }
}

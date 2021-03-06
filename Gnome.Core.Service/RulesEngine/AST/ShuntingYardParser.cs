﻿using Gnome.Core.Service.RulesEngine.Tokenizer;
using System.Collections.Generic;
using System.Linq;

namespace Gnome.Core.Service.RulesEngine.AST
{
    public class ShuntingYardParser
    {
        private readonly Lexer lexer;

        public ShuntingYardParser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public IEnumerable<IToken> Parse(string expression)
        {
            var tokens = lexer.GetTokens(expression);
            var output = new Queue<IToken>();
            var operators = new Stack<IToken>();

            foreach (var token in tokens.Where(t => (t is SkipToken) == false))
            {
                if (token is IOperand)
                    output.Enqueue(token);
                else if (token is IOperator o)
                {
                    while (operators.Count > 0
                        && operators.Peek() is IOperator top
                        && top.Precedence >= o.Precedence
                        && top.Associativity == Associativity.Left)
                    {
                        output.Enqueue(operators.Pop());
                    }

                    operators.Push(o);
                }
                else if (token is OpenParenthesisToken openParentheses)
                    operators.Push(openParentheses);
                else if (token is ClosingParenthesisToken closingParentheses)
                {
                    while (!(operators.Peek() is OpenParenthesisToken))
                        output.Enqueue(operators.Pop());
                    operators.Pop();
                }
            }
            while (operators.Count > 0)
                output.Enqueue(operators.Pop());

            foreach (var token in output)
            {
                yield return token;
            }
        }
    }
}

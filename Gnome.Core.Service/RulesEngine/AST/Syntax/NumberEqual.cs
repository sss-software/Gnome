﻿using Gnome.Core.Service.Transactions;
using System.Linq;

namespace Gnome.Core.Service.RulesEngine.AST.Syntax
{
    public class NumberEqual : ISyntaxNode<bool>
    {
        private readonly ISyntaxNode<decimal>[] numbers;

        public NumberEqual(params ISyntaxNode<decimal>[] numbers)
        {
            this.numbers = numbers;
        }

        public bool Evaluate(TransactionRow row)
        {
            return numbers.Select(n => n.Evaluate(row)).Distinct().Count() == 1;
        }
    }
}
﻿using Gnome.Core.Service.Transactions;
using System.Linq;

namespace Gnome.Core.Service.RulesEngine.AST.Syntax
{
    public class StringEquals : ISyntaxNode<bool>
    {
        private readonly ISyntaxNode<string>[] strings;

        public StringEquals(ISyntaxNode<string>[] strings)
        {
            this.strings = strings;
        }

        public bool Evaluate(TransactionRow row)
        {
            return strings.Select(s => s.Evaluate(row)).Distinct().Count() == 1;
        }
    }
}
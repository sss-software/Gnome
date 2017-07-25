﻿using Gnome.Core.Service.Transactions;

namespace Gnome.Core.Service.RulesEngine
{
    public class Or : IExpression
    {
        public IExpression LeftExpression { get; }
        public IExpression RightExpression { get; }

        public Or(IExpression left, IExpression right)
        {
            this.LeftExpression = left;
            this.RightExpression = right;
        }

        public bool Evaluate(TransactionRow transaction)
        {
            return LeftExpression.Evaluate(transaction) || RightExpression.Evaluate(transaction);
        }
    }
}
﻿namespace Gnome.Core.Service.RulesEngine.Tokenizer
{
    public class OpenParenthesisToken : IToken
    {
        public string Value { get; }
        public OpenParenthesisToken()
        {
            this.Value = "(";
        }
    }
}

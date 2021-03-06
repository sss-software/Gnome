﻿using System;

namespace Gnome.Core.Model.Database
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string CategoryData { get; set; }
    }
}
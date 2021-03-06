﻿using Gnome.Core.Model.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Gnome.Core.Service.Transactions.RowFactories
{
    public class TransactionFactory
    {
        private readonly TransactionTemplate template;

        public TransactionFactory(TransactionTemplate template)
        {
            this.template = template;
        }

        public TransactionRow Create(Transaction transaction)
        {
            var transactionRow = new TransactionRow(
                transaction.Id,
                transaction.AccountId,
                transaction.Date.Date,
                transaction.Amount,
                transaction.Type,
                JsonConvert.DeserializeObject<List<Guid>>(transaction.CategoryData));

            var jsonObject = JObject.Parse(transaction.Data);

            foreach (var field in template)
                transactionRow[field] = jsonObject[field]?.ToString();

            return transactionRow;
        }
    }
}

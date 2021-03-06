﻿using Gnome.Core.DataAccess;
using Gnome.Core.Model.Database;
using Gnome.Core.Service.Search.Filters;
using Gnome.Core.Service.Search.QueryBuilders;
using Gnome.Core.Service.Transactions.RowFactories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gnome.Core.Service.Transactions.QueryBuilders
{
    public interface ITransactionRowQueryBuilder
    {
        Task<IEnumerable<TransactionRow>> Query(Guid userId, TransactionSearchFilter filter);
    }

    public class TransactionRowQueryBuilder : ITransactionRowQueryBuilder
    {
        private readonly ITransactionRepository repository;
        private readonly IQueryBuilderService<Transaction, TransactionSearchFilter> queryBuilder;
        private readonly IAbstractTransactionFactory rowFactory;

        public TransactionRowQueryBuilder(
            ITransactionRepository repository,
            IQueryBuilderService<Transaction, TransactionSearchFilter> queryBuilder,
            IAbstractTransactionFactory rowFactory)
        {
            this.repository = repository;
            this.queryBuilder = queryBuilder;
            this.rowFactory = rowFactory;
        }

        public async Task<IEnumerable<TransactionRow>> Query(Guid userId, TransactionSearchFilter filter)
        {
            var transactionsQuery = queryBuilder.Filter(repository.Query, filter);
            return (await transactionsQuery
                .ToListAsync())
                .Select(t => rowFactory.Create(t));
        }
    }
}
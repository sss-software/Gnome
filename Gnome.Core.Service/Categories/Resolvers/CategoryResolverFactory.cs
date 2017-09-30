﻿using Gnome.Core.DataAccess;
using Gnome.Core.Model.Database;
using Gnome.Core.Service.Search.Filters;
using Gnome.Core.Service.Search.QueryBuilders;
using System;
using System.Linq;

namespace Gnome.Core.Service.Categories.Resolvers
{
    public interface ICategoryResolverFactory
    {
        Resolver Create(Guid userId, TransactionSearchFilter filter);
    }

    public class CategoryResolverFactory : ICategoryResolverFactory
    {
        private readonly ICategoryTreeFactory categoryTreeFactory;
        private readonly IQueryBuilderService<CategoryTransaction, TransactionSearchFilter> categoryTransactionQueryBuilderService;
        private readonly ICategoryTransactionRepository categoryTransactionRepository;

        public CategoryResolverFactory(
            ICategoryTreeFactory categoryTreeFactory,
            IQueryBuilderService<CategoryTransaction, TransactionSearchFilter> categoryTransactionQueryBuilderService,
            ICategoryTransactionRepository categoryTransactionRepository)
        {
            this.categoryTreeFactory = categoryTreeFactory;
            this.categoryTransactionQueryBuilderService = categoryTransactionQueryBuilderService;
            this.categoryTransactionRepository = categoryTransactionRepository;
        }

        public Resolver Create(Guid userId, TransactionSearchFilter filter)
        {
            var categoryTree = categoryTreeFactory.Create(userId);
            var categoryTransactionsQuery = categoryTransactionQueryBuilderService.Filter(categoryTransactionRepository.Query, filter);
            var transactionCategories = categoryTransactionsQuery.ToLookup(k => k.TransactionId, v => v.CategoryId);

            return new Resolver(categoryTree, transactionCategories);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Marten;
using Momentum.Framework.Core.Models;
using Momentum.Framework.Core.Repositories;

namespace Momentum.Framework.Infrastructure.Repositories
{
    public abstract class GenericTimeTrackedRepository<T> : IGenericRepository<T> where T : TimeTrackedModel
    {
        protected readonly IDocumentStore Store;

        protected GenericTimeTrackedRepository(IDocumentStore store)
        {
            Store = store;
        }

        public async Task<T> Add(T model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = null;

            using var session = Store.LightweightSession();

            session.Insert(model);

            await session.SaveChangesAsync();

            return model;
        }

        public async Task<T> Update(T model)
        {
            model.UpdatedAt = DateTime.UtcNow;

            using var session = Store.LightweightSession();

            session.Update(model);

            await session.SaveChangesAsync();

            return model;
        }
        public async Task Delete(T model)
        {
            using var session = Store.LightweightSession();

            session.Delete(model);

            await session.SaveChangesAsync();
        }

        protected async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> singleMatchExpression)
        {
            using var session = Store.QuerySession();

            return await session.Query<T>()
                .SingleOrDefaultAsync(singleMatchExpression);
        }

        protected async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> matchExpression)
        {
            using var session = Store.QuerySession();

            return await session.Query<T>()
                .Where(matchExpression)
                .ToListAsync();
        }
    }
}
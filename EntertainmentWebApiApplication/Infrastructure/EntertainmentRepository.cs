using EntertainmentWebApiApplication.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Infrastructure
{
    public class EntertainmentRepository<TContext> : IRepository
        where TContext : DbContext
    {
        private EntertainmentDataContext context;
        public EntertainmentRepository(EntertainmentDataContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> QueryinContext<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            IQueryable<TEntity> query = context.Set<TEntity>().AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                query = include(query);
            }

            return query;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            return await QueryinContext(include: include).ToListAsync();
        }

        public IEnumerable<TEntity> GetAll<TEntity>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            return QueryinContext(include: include).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            return await QueryinContext(filter: filter,
                include: include
                ).ToListAsync();
        }

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            return QueryinContext(filter: filter,
                include: include
                ).ToList();
        }

        public Task<TEntity> GetOneAsync<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            return QueryinContext(filter: filter,
                include: include
                ).FirstOrDefaultAsync();
        }

        public TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            return QueryinContext(filter: filter,
                include: include
                ).FirstOrDefault();
        }

        public bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            return QueryinContext(filter: filter).Any();
        }

        public Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            return QueryinContext(filter: filter).AnyAsync();
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Set<TEntity>().Remove(entity);
        }
        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            context.Set<TEntity>().Add(entity);
        }

        public void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }

        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }

        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            context.Database.CommitTransaction();
        }
    }
}

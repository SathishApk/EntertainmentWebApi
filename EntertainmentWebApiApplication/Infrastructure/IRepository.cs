using EntertainmentWebApiApplication.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Infrastructure
{
    public interface IRepository
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        where TEntity : class;
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class;
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class;
        Task<TEntity> GetOneAsync<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class;
        TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class;
        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        void Delete<TEntity>(TEntity entity)
            where TEntity : class;
        void Update<TEntity>(TEntity entity)
            where TEntity : class;
        void Create<TEntity>(TEntity entity)
            where TEntity : class;
        void SaveChanges();
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction();
    }
}

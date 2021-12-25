using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Accounting.WebAPI.Data.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        public RepositoryBase(AccountingContext accountingContext)
        {
            if (accountingContext == null)
            {
                throw new ArgumentNullException(paramName: nameof(accountingContext));
            }

            AccountingContext = accountingContext;

            DbSet = AccountingContext.Set<T>();
        }


        protected AccountingContext AccountingContext { get; }
        internal DbSet<T> DbSet { get; }


        public virtual async Task<IList<T>> GetAllAsync()
        {
            var result = await DbSet.ToListAsync();

            return result;
        }


        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(keyValues: id);
        }


        public virtual async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await DbSet.AddAsync(entity);
        }


        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                DbSet.Remove(entity);
            });
        }


        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            T entity =
                await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await DeleteAsync(entity);

            return true;
        }


        public virtual void Update(T entity)
        {
            /*Check if you have it already , check is there any difference , so the EF core statrs tracking to see that there are certain fields
             that are different so these are two different records*/
            DbSet.Attach(entity);
            /*Once we tell it that it has been modified then it will know that is ok I need to do an update to it*/
            AccountingContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IList<T>> GetAllUdemyAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = DbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetUdemyAsync(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = DbSet;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public async Task<IPagedList<T>> GetAllUdemyPagingAsync(RequestParams requestParams,List<string> includes = null)
        {
            IQueryable<T> query = DbSet;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }
    }
}

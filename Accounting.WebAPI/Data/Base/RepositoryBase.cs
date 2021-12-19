using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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


        public virtual IList<T> GetAll()
        {
            var result =
                DbSet.ToList();

            return result;
        }


        public virtual async Task<IList<T>> GetAllAsync()
        {
             var result = await DbSet.ToListAsync();

            return result;
        }


        public virtual T GetById(int id)
        {
            return DbSet.Find(keyValues: id);
        }


        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(keyValues: id);
        }


        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Add(entity);
        }


        public virtual async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await DbSet.AddAsync(entity);
        }


        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Remove(entity);

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


        public virtual bool DeleteById(int id)
        {
            T entity = GetById(id);

            if (entity == null)
            {
                return false;
            }

            Delete(entity);

            return true;
        }


        public virtual async System.Threading.Tasks.Task<bool> DeleteByIdAsync(int id)
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
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Update(entity);
        }


        public virtual async System.Threading.Tasks.Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                DbSet.Update(entity);
            });
        }

        public virtual IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? DbSet.AsNoTracking().OfType<T>() : DbSet;

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? DbSet.Where(expression).AsNoTracking() : DbSet.Where(expression);
    }
}

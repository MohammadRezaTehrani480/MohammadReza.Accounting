using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data.Base
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        void Insert(T entity);

        Task InsertAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        bool DeleteById(int id);

        Task<bool> DeleteByIdAsync(int id);

        IList<T> GetAll();

        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Moreover, we can see the trackChanges parameter. We are going to use
        /*it to improve our read-only query performance.When it’s set to false, we
        attach the AsNoTracking method to our query to inform EF Core that it
        doesn’t need to track changes for the required entities.This greatly
        improves the speed of a query.*/
        /// </summary>
        /// <param name="trackChanges"></param>
        /// <returns></returns>

        IQueryable<T> FindAll(bool trackChanges);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    }
}

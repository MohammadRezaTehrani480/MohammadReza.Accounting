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
        Task InsertAsync(T entity);

        Task InsertRange(IEnumerable<T> entities);

        void Update(T entity);

        Task DeleteAsync(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task<T> GetByIdAsync(int id);

        Task<bool> DeleteByIdAsync(int id);

        Task<IList<T>> GetAllAsync();

        /* Moreover, we can see the trackChanges parameter. We are going to use
        /*it to improve our read-only query performance.When it’s set to false, we
        attach the AsNoTracking method to our query to inform EF Core that it
        doesn’t need to track changes for the required entities.This greatly
        improves the speed of a query.*/

        Task<IList<T>> GetAllUdemyAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
         );

        Task<T> GetUdemyAsync(Expression<Func<T, bool>> expression, List<string> includes = null);
    }
}

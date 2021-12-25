using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Accounting.WebAPI.Data
{
    public class PersonRepository : Base.RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(AccountingContext accountingContext) : base(accountingContext)
        {
        }

        public async Task<IList<RealPerson>> GetAllRealPeopleUdemyAsync(Expression<Func<RealPerson, bool>> expression = null, Func<IQueryable<RealPerson>, IOrderedQueryable<RealPerson>> orderBy = null, List<string> includes = null)
        {
            IQueryable<RealPerson> query = DbSet.OfType<RealPerson>();

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


        public async Task<IList<LegalPerson>> GetAllLegalPeopleUdemyAsync(Expression<Func<LegalPerson, bool>> expression = null, Func<IQueryable<LegalPerson>, IOrderedQueryable<LegalPerson>> orderBy = null, List<string> includes = null)
        {
            IQueryable<LegalPerson> query = DbSet.OfType<LegalPerson>();

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


        public async Task<RealPerson> GetSingelRealPersonUdemyAsync(Expression<Func<RealPerson, bool>> expression, List<string> includes = null)
        {
            IQueryable<RealPerson> query = DbSet.OfType<RealPerson>();

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }


        public async Task<LegalPerson> GetSingelLegalPersonUdemyAsync(Expression<Func<LegalPerson, bool>> expression, List<string> includes = null)
        {
            IQueryable<LegalPerson> query = DbSet.OfType<LegalPerson>();

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IPagedList<RealPerson>> GetAllRealPeopleUdemyPagingAsync(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<RealPerson> query = DbSet.OfType<RealPerson>();

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task<IPagedList<LegalPerson>> GetAllLegalPeopleUdemyPagingAsync(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<LegalPerson> query = DbSet.OfType<LegalPerson>();

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

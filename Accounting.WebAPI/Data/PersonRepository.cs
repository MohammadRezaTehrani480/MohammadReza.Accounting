using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public class PersonRepository : Base.RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(AccountingContext accountingContext) : base(accountingContext)
        {
        }

        public IQueryable<RealPerson> Query(bool eager = false)
        {
            var query = DbSet.OfType<RealPerson>().AsQueryable();
            if (!eager)
            {
                foreach (var property in AccountingContext.Model.FindEntityType(typeof(RealPerson)).GetNavigations())
                    query = query.Include(property.Name);
            }
            return query;
        }

        //public async Task<IEnumerable<Person>> GetAllPeopleAsync(bool trackchanges)
        //{
        //    return await FindAll(trackchanges)
        //         .ToListAsync();
        //}

        //public async Task<Person> GetSingelPersonAsync(int personId, bool trackChanges)
        //{
        //    return await FindByCondition(c => c.Id.Equals(personId), trackChanges)
        //       .SingleOrDefaultAsync();
        //}

        public async Task<IEnumerable<RealPerson>> GetAllRealPeopleAsync(bool eager)
        {
            return await Query(eager).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task DeleteRealPersonAsync(RealPerson realPerson)
        {
            await DeleteAsync(realPerson);
        }

        public async Task<RealPerson> GetSingelRealPersonAsync(int realPersonId, bool eager)
        {
            return await Query(eager).SingleOrDefaultAsync(c => c.Id.Equals(realPersonId));
        }

        public async Task DeleteLegalPersonAsync(LegalPerson legalPerson)
        {
            await DeleteAsync(legalPerson);
        }

        public async Task<IEnumerable<LegalPerson>> GetAllLegalPeopleAsync()
        {
            return await DbSet.OfType<LegalPerson>()
                  .OrderBy(x => x.Id)
                  .ToListAsync();
        }

        public async Task<LegalPerson> GetSingelLegalPersonAsync(int legalPersonId)
        {
            return await DbSet.OfType<LegalPerson>()
                   .Where(c => c.Id.Equals(legalPersonId))
                   .SingleOrDefaultAsync();
        }

        //***************************************************************************
        /*Udemy functions*/
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
    }
}

using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Accounting.WebAPI.Data
{
    public interface IPersonRepository : Base.IRepositoryBase<Person>
    {
        Task<IList<RealPerson>> GetAllRealPeopleUdemyAsync(Expression<Func<RealPerson, bool>> expression = null,
            Func<IQueryable<RealPerson>, IOrderedQueryable<RealPerson>> orderBy = null,
            List<string> includes = null);

        Task<IList<LegalPerson>> GetAllLegalPeopleUdemyAsync(Expression<Func<LegalPerson, bool>> expression = null,
            Func<IQueryable<LegalPerson>, IOrderedQueryable<LegalPerson>> orderBy = null,
            List<string> includes = null);


        Task<RealPerson> GetSingelRealPersonUdemyAsync(Expression<Func<RealPerson, bool>> expression, List<string> includes = null);


        Task<LegalPerson> GetSingelLegalPersonUdemyAsync(Expression<Func<LegalPerson, bool>> expression, List<string> includes = null);


        Task<IPagedList<RealPerson>> GetAllRealPeopleUdemyPagingAsync(RequestParams requestParams, List<string> includes = null);


        Task<IPagedList<LegalPerson>> GetAllLegalPeopleUdemyPagingAsync(RequestParams requestParams, List<string> includes = null);
    }
}

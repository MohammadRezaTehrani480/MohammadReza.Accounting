using Accounting.WebAPI.Entities;
using Accounting.WebAPI.Extensions;
using Accounting.WebAPI.RequestFeatures;
using EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public class RealPersonRepository : Base.RepositoryBase<RealPerson>, IRealPersonRepository
    {
        public RealPersonRepository(AccountingContext accountingContext) : base(accountingContext)
        {
        }

        public async Task DeleteRealPersonAsync(RealPerson realPerson)
        {
            await DeleteAsync(realPerson);
        }

        public async Task<IEnumerable<RealPerson>> GetAllRealPeopleAsync(bool trackchanges) =>
           await FindAll(trackchanges)
                .Include(r => r.Cashes)
                .OrderBy(x => x.Id)
                .ToListAsync();


        public async Task<RealPerson> GetSingelRealPersonAsync(int realPersonId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(realPersonId), trackChanges)
              .Include(current => current.Nationality)
              .SingleOrDefaultAsync();
    }
}

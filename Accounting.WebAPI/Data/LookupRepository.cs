using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.WebAPI.Data
{
    public class LookupRepository : Base.RepositoryBase<Lookup>, ILookupRepository
    {
        public LookupRepository(AccountingContext accountingContext) : base(accountingContext)
        {
        }

        public async Task<IEnumerable<Lookup>> GetAllLookupsAsync(bool trackChanges) =>
           await FindAll(trackChanges)
                .OrderBy(x => x.Id)
                .ToListAsync();

        public async Task<Lookup> GetLookupAsync(int lookupId, bool trackChanges) =>
            await FindByCondition(c => c.Id==lookupId, trackChanges)
                  .SingleOrDefaultAsync();
    }
}

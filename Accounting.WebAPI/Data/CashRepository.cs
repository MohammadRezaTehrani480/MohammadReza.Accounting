using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public class CashRepository : Base.RepositoryBase<Cash>, ICashRepository
    {
        public CashRepository(AccountingContext accountingContext) : base(accountingContext)
        {
        }

        public async Task CreateCashForRealPersonAsync(int realPersonId, Cash cash)
        {
            cash.RealPersonId = realPersonId;
            await InsertAsync(cash);
        }

        public async Task DeleteCashAsync(Cash cash)
        {
            await DeleteAsync(cash);
        }

        public async Task<Cash> GetSingelCashAsync(int realPersonId, int id, bool trackChanges) =>
         await FindByCondition(c => c.RealPersonId.Equals(realPersonId) && c.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();


        public async Task<IEnumerable<Cash>> GetAllCashesAsync(int realPersonId, bool trackChanges) =>
            await FindByCondition(c => c.RealPersonId.Equals(realPersonId), trackChanges)
                  .OrderBy(c => c.CashName)
                  .ToListAsync();
    }
}

using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public class LegalPersonRepository : Base.RepositoryBase<LegalPerson>, ILegalPersonRepository
    {
        public LegalPersonRepository(AccountingContext accountingContext) : base(accountingContext)
        {

        }

        public async Task DeleteLegalPersonAsync(LegalPerson legalPerson)
        {
            await DeleteAsync(legalPerson);
        }

        public async Task<IEnumerable<LegalPerson>> GetAllLegalPeopleAsync(bool trackChanges) =>
        await FindAll(trackChanges)
              .OrderBy(x => x.Id)
              .ToListAsync();

        public async Task<LegalPerson> GetLegalPersonAsync(int legalPersonId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(legalPersonId), trackChanges)
              .SingleOrDefaultAsync();
    }
}

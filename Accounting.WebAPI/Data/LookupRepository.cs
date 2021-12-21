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

    }
}

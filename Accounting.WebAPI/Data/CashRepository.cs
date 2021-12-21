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

    }
}

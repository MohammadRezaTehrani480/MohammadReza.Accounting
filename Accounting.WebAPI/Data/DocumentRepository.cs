using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public class DocumentRepository : Base.RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(AccountingContext accountingContext) : base(accountingContext)
        {
        }

    }
}

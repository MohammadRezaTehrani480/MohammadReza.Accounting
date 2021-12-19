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

        public async Task CreateDocumentForPersonAndCashAsync(Document document)
        {
            await InsertAsync(document);
        }

        public async Task DeleteDocumentAsync(Document document)
        {
            await DeleteAsync(document);
        }

        public async Task<Document> GetSingelDocumentAsync(int personId, int cashId, int id, bool trackChanges) =>
        await FindByCondition(d => d.PersonId.Equals(personId) && d.Id.Equals(id) && d.CashId.Equals(cashId), trackChanges)
              .Include(current => current.Cash)
              //.Include(current =>current.AccountSide)
              //.Include(current => current.DocType)
              .SingleOrDefaultAsync();

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync(int personId, int cashId, bool trackChanges) =>
        await FindByCondition(d => d.PersonId.Equals(personId) && d.CashId.Equals(cashId), trackChanges)
                  .Include(current => current.Cash)
                  //.Include(current => current.AccountSide)
                  //.Include(current => current.DocType)
                  .OrderBy(d => d.Id)
                  .ToListAsync();
    }
}

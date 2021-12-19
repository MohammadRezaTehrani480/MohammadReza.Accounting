using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public interface IDocumentRepository : Base.IRepositoryBase<Document>
    {
        Task<IEnumerable<Document>> GetAllDocumentsAsync(int personId, int cashId, bool trackChanges);
        Task<Document> GetSingelDocumentAsync(int personId, int cashId, int id, bool trackChanges);
        Task CreateDocumentForPersonAndCashAsync(Document document);
        Task DeleteDocumentAsync(Document document);
    }
}

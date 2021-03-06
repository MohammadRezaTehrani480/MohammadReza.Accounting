using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public interface IUnitOfWork : Base.IUnitOfWorkBase
    {
        IPersonRepository PersonRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        ICashRepository CashRepository { get; }
        ILookupRepository LookupRepository { get; }
    }
}

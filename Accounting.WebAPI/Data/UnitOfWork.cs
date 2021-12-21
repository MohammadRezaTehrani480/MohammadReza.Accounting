using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public class UnitOfWork : Base.UnitOfWorkBase, IUnitOfWork
    {
        public UnitOfWork(Tools.Options options) : base(options)
        {
        }

        private IPersonRepository _personRepository;
        private IDocumentRepository _documentRepository;
        private ICashRepository _cashRepository;
        private ILookupRepository _lookupRepository;


        public IPersonRepository PersonRepository =>_personRepository ??= new PersonRepository(AccountingContext);
        public IDocumentRepository DocumentRepository => _documentRepository ??= new DocumentRepository(AccountingContext);
        public ICashRepository CashRepository => _cashRepository ??= new CashRepository(AccountingContext);
        public ILookupRepository LookupRepository => _lookupRepository ??= new LookupRepository(AccountingContext);
    }
}

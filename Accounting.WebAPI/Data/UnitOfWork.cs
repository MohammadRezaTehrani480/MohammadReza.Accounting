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

        //private IRealPersonRepository _realPersonRepository;

        //public IRealPersonRepository RealPersonRepository
        //{
        //    get
        //    {
        //        if (_realPersonRepository == null)
        //        {
        //            _realPersonRepository = new RealPersonRepository(AccountingContext);
        //        }

        //        return _realPersonRepository;
        //    }
        //}


        //private ILegalPersonRepository _legalPersonRepository;

        //public ILegalPersonRepository LegalPersonRepository
        //{
        //    get
        //    {
        //        if (_legalPersonRepository == null)
        //        {
        //            _legalPersonRepository = new LegalPersonRepository(AccountingContext);
        //        }

        //        return _legalPersonRepository;
        //    }
        //}

        private IPersonRepository _personRepository;

        public IPersonRepository PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new PersonRepository(AccountingContext);
                }

                return _personRepository;
            }
        }


        private IDocumentRepository _documentRepository;

        public IDocumentRepository DocumentRepository
        {
            get
            {
                if (_documentRepository == null)
                {
                    _documentRepository = new DocumentRepository(AccountingContext);
                }

                return _documentRepository;
            }
        }


        private ICashRepository _cashRepository;

        public ICashRepository CashRepository
        {
            get
            {
                if (_cashRepository == null)
                {
                    _cashRepository = new CashRepository(AccountingContext);
                }

                return _cashRepository;
            }
        }


        private ILookupRepository _lookupRepository;

        public ILookupRepository LookupRepository
        {
            get
            {
                if (_lookupRepository == null)
                {
                    _lookupRepository = new LookupRepository(AccountingContext);
                }

                return _lookupRepository;
            }
        }
    }
}

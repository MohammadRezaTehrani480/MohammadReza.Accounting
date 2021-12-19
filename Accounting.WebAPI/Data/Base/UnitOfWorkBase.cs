using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounting.WebAPI.Data.Tools;
using Accounting.WebAPI.Enum;

namespace Accounting.WebAPI.Data.Base
{
    public abstract class UnitOfWorkBase : IUnitOfWorkBase
    {

        public UnitOfWorkBase(Options options) : base()
        {
            Options = options;
        }


        protected Options Options { get; set; }


        private AccountingContext _accountingContext;


        internal AccountingContext AccountingContext
        {
            get
            {
                if (_accountingContext == null)
                {
                    var optionsBuilder =
                        new DbContextOptionsBuilder<AccountingContext>();

                    switch (Options.Provider)
                    {
                        case Provider.SqlServer:
                            {
                                optionsBuilder.UseSqlServer
                                    (connectionString: Options.ConnectionString);

                                break;
                            }

                        case Provider.MySql:
                            {
                                //optionsBuilder.UseMySql
                                //	(connectionString: Options.ConnectionString);

                                break;
                            }

                        case Provider.Oracle:
                            {
                                //optionsBuilder.UseOracle
                                //	(connectionString: Options.ConnectionString);

                                break;
                            }

                        case Provider.PostgreSQL:
                            {
                                //optionsBuilder.UsePostgreSQL
                                //	(connectionString: Options.ConnectionString);

                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }

                    _accountingContext =
                        new AccountingContext(options: optionsBuilder.Options);
                }

                return _accountingContext;
            }
        }

        public bool IsDisposed { get; protected set; }


        public void Dispose()
        {
            Dispose(true);

            System.GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {

                if (_accountingContext != null)
                {
                    _accountingContext.Dispose();
                    _accountingContext = null;
                }
            }

            IsDisposed = true;
        }


        ~UnitOfWorkBase()
        {
            Dispose(false);
        }


        //public RepositoryBase<T> GetRepository<T>() where T : BaseEntity
        //{
        //    return new RepositoryBase<T>(accountingContext: AccountingContext);
        //}


        public virtual void Save()
        {
            AccountingContext.SaveChanges();
        }


        public virtual async System.Threading.Tasks.Task SaveAsync()
        {
            await AccountingContext.SaveChangesAsync();
        }
    }
}

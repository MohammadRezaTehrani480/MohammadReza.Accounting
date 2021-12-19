using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data.Base
{
    public interface IUnitOfWorkBase : IDisposable
    {
        bool IsDisposed { get; }

        void Save();

        Task SaveAsync();

        //RepositoryBase<T> GetRepository<T>() where T : BaseEntity;
    }
}

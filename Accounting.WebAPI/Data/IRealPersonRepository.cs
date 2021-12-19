using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.RequestFeatures;
using System.Linq.Expressions;
using EntityFrameworkPaginateCore;

namespace Accounting.WebAPI.Data
{
    public interface IRealPersonRepository : Base.IRepositoryBase<RealPerson>
    {
        Task<IEnumerable<RealPerson>> GetAllRealPeopleAsync(bool trackChanges);
        Task<RealPerson> GetSingelRealPersonAsync(int realPersonId, bool trackChanges);
        Task DeleteRealPersonAsync(RealPerson realPerson);
    }
}

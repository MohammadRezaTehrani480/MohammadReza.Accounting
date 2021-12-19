using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public interface ICashRepository : Base.IRepositoryBase<Cash>
    {
        Task<IEnumerable<Cash>> GetAllCashesAsync(int realPersonId, bool trackChanges);
        Task<Cash> GetSingelCashAsync(int realPersonId, int id, bool trackChanges);
        Task CreateCashForRealPersonAsync(int realPersonId, Cash cash);
        Task DeleteCashAsync(Cash cash);
    }
}

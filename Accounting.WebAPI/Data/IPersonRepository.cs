using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public interface IPersonRepository : Base.IRepositoryBase<Person>
    {
        IQueryable<RealPerson> Query(bool eager = false);
        Task<IEnumerable<Person>> GetAllPeopleAsync(bool trackchanges);
        Task<Person> GetSingelPersonAsync(int personId, bool trackChanges);
        Task<IEnumerable<RealPerson>> GetAllRealPeopleAsync(bool eager);
        Task<RealPerson> GetSingelRealPersonAsync(int realPersonId, bool eager);
        Task DeleteRealPersonAsync(RealPerson realPerson);
        Task<IEnumerable<LegalPerson>> GetAllLegalPeopleAsync();
        Task<LegalPerson> GetSingelLegalPersonAsync(int legalPersonId);
        Task DeleteLegalPersonAsync(LegalPerson legalPerson);
    }
}

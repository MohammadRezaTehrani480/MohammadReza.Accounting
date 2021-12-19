using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Entities;

namespace Accounting.WebAPI.Data
{
    public interface ILegalPersonRepository : Base.IRepositoryBase<LegalPerson>
    {
        Task<IEnumerable<LegalPerson>> GetAllLegalPeopleAsync(bool trackChanges);

        Task<LegalPerson> GetLegalPersonAsync(int legalPersonId, bool trackChanges);

        Task DeleteLegalPersonAsync(LegalPerson legalPerson);
    }
}

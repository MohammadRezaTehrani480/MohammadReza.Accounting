using Accounting.Shared.ViewModels.CashViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.RealPersonViewModels
{
    public class RealPersonDTO : BaseModel
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual LookupDTO BirthPlace { get; set; }
        public int BirthPlaceId { get; set; }
        public DateTime BirthDate { get; set; }
        public string FatherName { get; set; }
        public int NationalityId { get; set; }
        public virtual LookupDTO Nationality { get; set; }
        public string Title { get; set; }
        public int Age { get; set; }
        public virtual ICollection<CashDto> Cashes { get; set; }
    }
}

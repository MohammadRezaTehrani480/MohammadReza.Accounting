using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Entities
{
    public class RealPerson : Person
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FatherName { get; set; }
        public int Age { get; set; }
        //=====================================================
        public int BirthPlaceId { get; set; }
        public virtual Lookup BirthPlace { get; set; }
        public int NationalityId { get; set; }
        public virtual Lookup Nationality { get; set; }
        public virtual ICollection<Cash> Cashes { get; set; }
    }
}

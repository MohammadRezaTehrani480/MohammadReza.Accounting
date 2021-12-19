using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Entities
{
    public class LegalPerson : Person
    {
        public int CompanyNo { get; set; }
        public int RegistrationCode { get; set; }
        public int EconomicCode { get; set; }
    }
}

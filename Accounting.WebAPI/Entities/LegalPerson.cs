using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Entities
{
    public class LegalPerson : Person
    {
        public string CompanyNo { get; set; }
        public string RegistrationCode { get; set; }
        public string EconomicCode { get; set; }
    }
}

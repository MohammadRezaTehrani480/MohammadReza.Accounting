using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.LegalPersonViewModels
{
    public class LegalPersonDTO : BaseModel
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CompanyNo { get; set; }
        public int RegistrationCode { get; set; }
        public int EconomicCode { get; set; }
        public string Title { get; set; }
    }
}

using Accounting.Shared.ViewModels.CashViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.RealPersonViewModels
{
    public class RealPersonDTO : RealPersonCreationDTO
    {
        public int Id { get; set; }
        public LookupDTO BirthPlace { get; set; }
        public LookupDTO Nationality { get; set; }
    }
}

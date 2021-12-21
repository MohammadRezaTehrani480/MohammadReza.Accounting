using Accounting.Shared.ViewModels.RealPersonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.CashViewModels
{
    public class CashDTO : CashCreationDTO
    {
        public int Id { get; set; }
        public RealPersonDTO Cashier { get; set; }
    }
}

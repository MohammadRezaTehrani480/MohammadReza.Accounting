using Accounting.Shared.ViewModels.CashViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.RealPersonViewModels
{
    public class RealPersonUpdateDTO : RealPersonCreationDTO
    {
        public IList<CashCreationDTO> Cashes { get; set; }
    }
}

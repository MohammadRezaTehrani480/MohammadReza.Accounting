using Accounting.Shared.ViewModels.CashViewModels;
using Accounting.Shared.ViewModels.PersonViewModels;
using Accounting.Shared.ViewModels.RealPersonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.DocumentsViewModels
{
    public class DocumentCreationDTO
    {
        public int DocNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int CashId { get; set; }
        public int PersonId { get; set; }
        public int DocTypeId { get; set; }
    }
}

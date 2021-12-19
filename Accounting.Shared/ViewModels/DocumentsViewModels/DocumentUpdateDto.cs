using Accounting.Shared.ViewModels.CashViewModels;
using Accounting.Shared.ViewModels.RealPersonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.DocumentsViewModels
{
    public class DocumentUpdateDto
    {
        public int DocNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public virtual CashDto Cash { get; set; }
        public int CashId { get; set; }
        public int PersonId { get; set; }
        public virtual LookupDTO DocType { get; set; }
        public int DocTypeId { get; set; }
    }
}

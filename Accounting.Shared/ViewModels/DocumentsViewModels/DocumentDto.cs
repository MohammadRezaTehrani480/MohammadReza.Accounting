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
    public class DocumentDTO : DocumentCreationDTO
    {
        public int Id { get; set; }
        public CashDTO Cash { get; set; }
        public PersonDTO AccountSide { get; set; }
        public LookupDTO DocType { get; set; }

    }
}

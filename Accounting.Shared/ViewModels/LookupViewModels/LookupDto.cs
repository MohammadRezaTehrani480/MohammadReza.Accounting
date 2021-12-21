using Accounting.Shared.ViewModels.DocumentsViewModels;
using Accounting.Shared.ViewModels.RealPersonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels
{
    public class LookupDTO : LookupCreationDTO
    {
        public int Id { get; set; }
        public IList<RealPersonDTO> RealPeople { get; set; }
        public IList<DocumentDTO> Documents { get; set; }
    }
}

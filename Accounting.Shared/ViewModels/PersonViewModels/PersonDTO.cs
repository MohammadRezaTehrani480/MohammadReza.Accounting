using Accounting.Shared.ViewModels.DocumentsViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.PersonViewModels
{
    public class PersonDTO
    {
        public IList<DocumentDTO> Documents { get; set; }
    }
}

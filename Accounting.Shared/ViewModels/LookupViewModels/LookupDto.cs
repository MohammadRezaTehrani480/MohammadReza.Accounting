using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels
{
    public class LookupDTO : BaseModel
    {
        public string Title { get; set; }
        public int LookupTypeId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Entities
{
    public class Cash : BaseEntity
    {
        public string CashName { get; set; }
        //==================================================
        public int RealPersonId { get; set; }
        public virtual RealPerson Cashier { get; set; }
        public virtual ICollection<Document> Documents { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Entities
{
    public class Document : BaseEntity
    {
        public int DocNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        //========================================================
        public int CashId { get; set; }
        public virtual Cash Cash { get; set; }
        public int PersonId { get; set; }
        public virtual Person AccountSide { get; set; }
        public int DocTypeId { get; set; }
        public virtual Lookup DocType { get; set; }
    }
}

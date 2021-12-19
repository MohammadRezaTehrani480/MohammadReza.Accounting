using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Entities
{
    public class Person : BaseEntity
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //public string Title { get; set; }
        //=============================================================
        public virtual ICollection<Document> Documents { get; set; }
    }
}

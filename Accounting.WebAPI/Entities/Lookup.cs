using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Enum;

namespace Accounting.WebAPI.Entities
{
    public class Lookup : BaseEntity
    {
        public string Title { get; set; }
        public int LookupTypeId { get; set; }
        public ICollection<RealPerson> RealPeople { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}

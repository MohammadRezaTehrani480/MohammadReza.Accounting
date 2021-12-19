using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.RequestFeatures
{
    public class RealPersonParameters : RequestParameters
    {
        public RealPersonParameters()
        {
        }
        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxAge > MinAge;
        public string SearchTerm { get; set; }
    }
}

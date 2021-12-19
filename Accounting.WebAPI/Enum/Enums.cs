using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Enum
{
    public enum LookupType : int
    {
        Documents = 1,
        Nationalities = 2,
        Cities = 3
    }

    public enum Provider : int
    {
        SqlServer = 0,
        MySql = 1,
        PostgreSQL = 2,
        Oracle = 3,
        InMemory = 4,
    }
}

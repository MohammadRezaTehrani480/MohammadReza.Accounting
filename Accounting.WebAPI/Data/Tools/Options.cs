using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Enum;

namespace Accounting.WebAPI.Data.Tools
{
    public class Options
    {
		public Options() : base()
		{
		}

		public Provider Provider { get; set; }


		public string ConnectionString { get; set; }
	}
}

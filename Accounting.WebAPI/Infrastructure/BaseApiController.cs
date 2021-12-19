using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Infrastructure
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController() : base()
        {
        }
    }
}

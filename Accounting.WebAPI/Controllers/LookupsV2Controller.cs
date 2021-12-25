using Accounting.WebAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    //[Route("api/{v:apiversion}/Lookups")]
    [Route("api/Lookups")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")] 
    public class LookupsV2Controller : ControllerBase
    {
        private AccountingContext _accountingContext;

        public LookupsV2Controller(AccountingContext accountingContext)
        {
            _accountingContext = accountingContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllLookupsAsync()
        {
            return Ok(_accountingContext.Lookups);
        }
    }
}

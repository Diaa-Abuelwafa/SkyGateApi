using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyGateDomainLayer.Errors;
using System.Net;

namespace SkyGateApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error()
        {
            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound, "NotFound EndPoint"));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Errors;

namespace VetHub02.Controllers
{
    [Route("Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseApiController
    {
        public ActionResult Error(int code)     
        {
                return NotFound(new ApiError(code));
        }
    }
}

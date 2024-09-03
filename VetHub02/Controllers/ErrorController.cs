using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Errors;
using VetHub02.Repository.Data;

namespace VetHub02.Controllers
{
 
    public class ErrorController : BaseApiController
    {
        private readonly StoreContext dbcontext;

        public ErrorController(StoreContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet("notFound")]
        public ActionResult GetNotFoundError()
        {
            var user = dbcontext.Users.Find(1000);
            if (user is null) return NotFound(new ApiError(404)); 
            return NotFound(user);
        }
        [HttpGet("badRequest")]
        public ActionResult GetbadrequestError() 
        {
            return BadRequest(new ApiError(400));
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult GetVaildtionError(int id )
        {
            return BadRequest(new ApivalidationError());
        }



    }
}

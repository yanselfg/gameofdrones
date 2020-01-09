using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameOfDrones.Web.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        public BaseController() : base()
        {

        }

        protected JsonResult SuccessResponse(string statusDescription, object data)
        {
            return new JsonResult(new
            {
                StatusCode = StatusCodes.Status200OK,
                StatusDescription = statusDescription,
                Data = data
            });
        }

        protected JsonResult ErrorResponse(string statusDescription)
        {
            return new JsonResult(new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                StatusDescription = statusDescription
            });
        }
    }
}
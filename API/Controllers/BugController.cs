using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class BugController : ApiBaseController
    {
        private readonly StoreContext _context;

        public BugController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult NotFoundMethod()
        {
            var product = _context.Products.Find(43);
            if (product != null)
            {
                return Ok();
            }
            return NotFound(new ApiResponseModel(404));
        }

        [HttpGet("internalerror")]
        public ActionResult InternalError()
        {
            var product = _context.Products.Find(43);
            var pr = product.Name.ToString();
            return Ok();
        }


        [HttpGet("badrequesterror")]
        public ActionResult BadRequestError()
        {
           return BadRequest(new ApiResponseModel(400));
        }

         [HttpGet("methodnotexist/{id}")]
        public ActionResult MethodNotExist(int id)
        {
           return BadRequest(new ApiResponseModel(400));
        }


    }
}
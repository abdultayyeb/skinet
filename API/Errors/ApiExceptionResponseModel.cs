using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiExceptionResponseModel : ApiResponseModel
    {
        public ApiExceptionResponseModel(int statusCode, string message = null, string details =null ) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}
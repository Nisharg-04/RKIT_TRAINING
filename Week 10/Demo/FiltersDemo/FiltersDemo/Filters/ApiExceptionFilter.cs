using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace FiltersDemo.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Debug.WriteLine("ExceptionFilter - OnException");

            context.Response =
                context.Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    "Unexpected error");
        }
    }
}
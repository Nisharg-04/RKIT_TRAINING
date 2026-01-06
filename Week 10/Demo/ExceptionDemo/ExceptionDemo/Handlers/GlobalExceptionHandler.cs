using ExceptionDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace ExceptionDemo.Handlers
{
    public class GlobalExceptionHandler: ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new ApiError
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred",
                    ErrorCode = "INTERNAL_SERVER_ERROR"
                });

            context.Result = new ResponseMessageResult(response);
        }
    }
}
using ExceptionDemo.Exceptions;
using ExceptionDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http.Filters;

namespace ExceptionDemo.Filters
{
    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            base.OnException(context);
            if (context.Exception is BusinessException be)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new ApiError
                    {
                        StatusCode = 400,
                        Message = be.Message,
                        ErrorCode = be.ErrorCode
                    });
            }
        }
    }
}
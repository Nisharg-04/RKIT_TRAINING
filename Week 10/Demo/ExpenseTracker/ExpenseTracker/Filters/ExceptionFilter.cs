using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using ExpenseTracker.Models.Logging;

namespace ExpenseTracker.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly INLogLogger _logger;

        public ExceptionFilter(INLogLogger logger)
        {
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            _logger.Error("EXCEPTION CAUGHT in Exception Filter");

            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                "Something went wrong");
        }
    }

}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace InterfaceBasedFilters.Filters
{
    public class SimpleAuthorizationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
         HttpActionContext actionContext,
         CancellationToken cancellationToken,
         Func<Task<HttpResponseMessage>> continuation)
        {
            Debug.WriteLine("Authorization Filter Running");

            var user = actionContext.RequestContext.Principal;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                // STOP PIPELINE
                return Task.FromResult(
                    actionContext.Request.CreateResponse(
                        HttpStatusCode.Unauthorized,
                        "User not authorized"
                    ));
            }

            // CONTINUE PIPELINE
            return continuation();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace InterfaceBasedFilters.Filters
{
    public class LoggingActionFilter : IActionFilter
    {

        public bool AllowMultiple => true;

        public async  Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            // BEFORE action
            Debug.WriteLine("Before Action");

            // Continue pipeline
            HttpResponseMessage response = await continuation();

            // AFTER action
            Debug.WriteLine("After Action");

            return response;
        }
    }
}
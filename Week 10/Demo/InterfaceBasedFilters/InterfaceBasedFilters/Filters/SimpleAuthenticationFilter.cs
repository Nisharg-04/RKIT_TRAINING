using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace InterfaceBasedFilters.Filters
{
    public class SimpleAuthenticationFilter : IAuthenticationFilter
    {
        public bool AllowMultiple =>false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        { // Normally  validate token here
            Debug.WriteLine("Authentication Filter Running");
            var identity = new ClaimsIdentity("CustomAuth");
            identity.AddClaim(new Claim(ClaimTypes.Name, "Nisharg"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            context.Principal = new ClaimsPrincipal(identity);
            return Task.CompletedTask;
        }

        public Task ChallengeAsync(
     HttpAuthenticationChallengeContext context,
     CancellationToken cancellationToken)
        {
            // ONLY challenge when response is 401
            if (context.Result is UnauthorizedResult)
            {
                context.Result = new UnauthorizedResult(
                    new[] { new AuthenticationHeaderValue("Bearer") },
                    context.Request);
            }

            return Task.CompletedTask;
        }

    }
}
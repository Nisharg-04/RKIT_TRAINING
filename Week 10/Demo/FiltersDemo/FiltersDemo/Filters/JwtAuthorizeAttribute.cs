using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FiltersDemo.Filters
{
    public class JwtAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            Debug.WriteLine("JwtAuthorize - OnAuthorization");

            
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, "admin"),
            new Claim(ClaimTypes.Role, "Admin")
        }, "Jwt");

            var principal = new ClaimsPrincipal(identity);

            actionContext.RequestContext.Principal = principal;
            Thread.CurrentPrincipal = principal;
        }
    }

}
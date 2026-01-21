using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RegenerateTokenDemo.Filters
{
    public class JwtAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext context)
        {
            var auth = context.Request.Headers.Authorization;
            if (auth == null)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(auth.Parameter,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_1234561111111111111"))
                    },
                    out _);
            }
            catch
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }

}
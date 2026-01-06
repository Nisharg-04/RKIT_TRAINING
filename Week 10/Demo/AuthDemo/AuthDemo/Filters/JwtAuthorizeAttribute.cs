using AuthDemo.App_Start;
using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    namespace AuthDemo.Filters
    {
        public class JwtAuthorizeAttribute : AuthorizationFilterAttribute
        {
        private string SecretKey =
            JwtConfig.SecretKey;

            public override void OnAuthorization(HttpActionContext actionContext)
            {
                var authHeader = actionContext.Request.Headers.Authorization;

                if (authHeader == null || authHeader.Scheme != "Bearer")
                {
                    actionContext.Response =
                        actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    return;
                }

                var token = authHeader.Parameter;
                try
                {
                var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(SecretKey);

                    var principal = tokenHandler.ValidateToken(token,
                        new TokenValidationParameters
                        {
                            ValidateIssuer = false,  
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ClockSkew = TimeSpan.Zero
                        },
                        out _);

        
                    actionContext.RequestContext.Principal = principal;
                    Thread.CurrentPrincipal = principal;
                }
                catch
                {
                    actionContext.Response =
                        actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }

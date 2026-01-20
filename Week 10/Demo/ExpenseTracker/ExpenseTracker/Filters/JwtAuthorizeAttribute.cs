using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ExpenseTracker.App_Start;

public class JwtAuthorizeAttribute : AuthorizationFilterAttribute
{
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var authHeader = actionContext.Request.Headers.Authorization;

        if (authHeader == null || authHeader.Scheme != "Bearer")
        {
            actionContext.Response =
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Token missing");
            return;
        }

        try
        {
            var token = authHeader.Parameter;
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = JwtConfig.Issuer,
                ValidAudience = JwtConfig.Audience,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey)),

                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParams, out validatedToken);

            // 🔑 THIS IS THE MOST IMPORTANT LINE
            HttpContext.Current.User = principal;
        }
        catch
        {
            actionContext.Response =
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
        }
    }
}

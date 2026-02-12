using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OAuthDemo.Controllers
{
    [ApiController]
    [Route("api/google")]
    public class GoogleController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {

            var props = new AuthenticationProperties
            {
                RedirectUri = "/api/google/response"
            };

            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;


            return Ok(new { email, name });
        }

    }

}

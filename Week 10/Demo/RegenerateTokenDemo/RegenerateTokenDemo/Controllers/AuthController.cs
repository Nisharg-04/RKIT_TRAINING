using RegenerateTokenDemo.Helpers;
using RegenerateTokenDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ServiceStack.OrmLite;
using System.Runtime.InteropServices.WindowsRuntime;

namespace RegenerateTokenDemo.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginDto dto)
        {
            using (var db = DbFactory.ConnectionFactory.Open())
            {
                var password = PasswordHashHelper.Hash(dto.Password);
                var user = db.Single<Users>(x => x.Username == dto.Username && x.PasswordHash == password);

                if (user == null) return Unauthorized();

                var accessToken = JwtHelper.GenerateAccessToken(user);

                var refreshToken = RefreshTokenHelper.Generate();
                var refreshTokenHash = RefreshTokenHelper.Hash(refreshToken);

                db.Insert(new RefreshTokens
                {
                    UserId = user.Id,
                    TokenHash = refreshTokenHash,
                    DeviceId = GetDeviceId(),
                    ExpiresAt = DateTime.UtcNow.AddDays(30),
                    CreatedAt = DateTime.UtcNow
                });

                SetRefreshCookie(refreshToken);

                return Ok(new { accessToken = accessToken });
            }
        }
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(RegisterDto dto)
        {
            var user = new Users
            {
                Username = dto.Username,
                PasswordHash = PasswordHashHelper.Hash(dto.Password)
            };
            using(var db = DbFactory.ConnectionFactory.Open())
            {
                db.Insert<Users>(user);
            }
            return Ok(new
            {
                message="User Registed Succesfully",
                user = user
            });

        }
        [HttpPost]
        [Route("refresh")]
        public IHttpActionResult Refresh()
        {
            var cookie = HttpContext.Current.Request.Cookies["refreshToken"];
            if (cookie == null) return Unauthorized();

            var hash = RefreshTokenHelper.Hash(cookie.Value);

            using (var db = DbFactory.ConnectionFactory.Open())
            {
                var deviceId = GetDeviceId();
                var token = db.Single<RefreshTokens>(
                    x => x.TokenHash == hash && !x.IsRevoked && x.DeviceId == deviceId);

                if (token == null || token.ExpiresAt < DateTime.UtcNow)
                    return Unauthorized();

                // rotation of refresh token for secyrity reason
                var newRefresh = RefreshTokenHelper.Generate();
                var newHash = RefreshTokenHelper.Hash(newRefresh);

                token.IsRevoked = true;
                token.ReplacedByTokenHash = newHash;
                db.Update(token);

                db.Insert(new RefreshTokens
                {
                    UserId = token.UserId,
                    TokenHash = newHash,
                    DeviceId = token.DeviceId,
                    ExpiresAt = DateTime.UtcNow.AddDays(30),
                    CreatedAt = DateTime.UtcNow
                });

                SetRefreshCookie(newRefresh);

                var user = db.SingleById<Users>(token.UserId);
                var accessToken = JwtHelper.GenerateAccessToken(user);

                return Ok(new { accessToken });
            }
        }

        private void SetRefreshCookie(string token)
        {
            var cookie = new HttpCookie("refreshToken", token)
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private string GetDeviceId()
        {
            return HttpContext.Current.Request.UserAgent;
        }
    }

}
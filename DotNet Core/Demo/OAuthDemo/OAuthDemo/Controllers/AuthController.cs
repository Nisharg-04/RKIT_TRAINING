using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuthDemo.Data;
using OAuthDemo.Models;
using OAuthDemo.Services;
using ServiceStack.OrmLite;

namespace OAuthDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ServiceStack.OrmLite;

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly DbFactory _db;
        private readonly JwtService _jwt;
        private readonly TwoFactorService _twofa;

        public AuthController(DbFactory db, JwtService jwt, TwoFactorService twofa)
        {
            _db = db;
            _jwt = jwt;
            _twofa = twofa;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            using var db = _db.Create().Open();

            var user = new User
            {
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                CreatedAt = DateTime.UtcNow
            };

            await db.InsertAsync(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            using var db = _db.Create().Open();

            var user = await db.SingleAsync<User>(x => x.Email == req.Email);

            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized();

            if (user.IsTwoFactorEnabled)
            {
                if (!_twofa.Validate(user.TwoFactorSecret, req.TwoFactorCode))
                    return Unauthorized("Invalid 2FA");
            }

            var access = _jwt.GenerateAccessToken(user);
            var refresh = _jwt.GenerateRefreshToken();

            await db.InsertAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refresh,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new { access, refresh });
        }
    }


}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuthDemo.Data;
using OAuthDemo.Models;
using OAuthDemo.Services;
using ServiceStack.OrmLite;

namespace OAuthDemo.Controllers
{
    [ApiController]
    [Route("api/2fa")]
    public class TwoFactorController : ControllerBase
    {
        private readonly DbFactory _db;
        private readonly TwoFactorService _twofa;

        public TwoFactorController(DbFactory db, TwoFactorService twofa)
        {
            _db = db;
            _twofa = twofa;
        }

        [HttpPost("setup/{userId}")]
        public async Task<IActionResult> Setup(int userId)
        {
            using var db = _db.Create().Open();

            var user = await db.SingleByIdAsync<User>(userId);

            var secret = _twofa.GenerateSecret();
            user.TwoFactorSecret = secret;

            await db.UpdateAsync(user);

            var qr = _twofa.GenerateQrCode(user.Email, secret);

            return File(qr, "image/png");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(int userId, string code)
        {
            using var db = _db.Create().Open();

            var user = await db.SingleByIdAsync<User>(userId);

            if (!_twofa.Validate(user.TwoFactorSecret, code))
                return BadRequest("Invalid Code");

            user.IsTwoFactorEnabled = true;
            await db.UpdateAsync(user);

            return Ok();
        }
    }

}

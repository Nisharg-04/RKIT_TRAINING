using InventoryMgt.BAL.Interfaces;
using InventoryMgt.MAL.DTO;
using InventoryMgt.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgt.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly JwtTokenGenerator _jwt;
        private readonly IConfiguration _config;

        public AuthController(
            IUserService service,
            JwtTokenGenerator jwt,
            IConfiguration config)
        {
            _service = service;
            _jwt = jwt;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDTO req)
        {
            var user = _service.Authenticate(req.Username, req.Password);
            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                token = _jwt.Generate(user, _config),
                role = user.Role
            });
        }
    }
}

using ExpenseTracker.BAL;
using ExpenseTracker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseTracker.Controllers
{
    [RoutePrefix("api/v1/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(RegisterUserDTO request)
        {
            _authService.Register(request);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginUserDTO request)
        {
            var token = _authService.Login(request);
            return Ok(new LoginResponseDTO { Token = token });
        }
    }
}
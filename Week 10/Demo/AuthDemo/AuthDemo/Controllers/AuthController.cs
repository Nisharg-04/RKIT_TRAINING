using AuthDemo.Models;
using AuthDemo.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace AuthDemo.Controllers
{

    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(UserModel model)
        {
            if (UserStore.UserExists(model.Username))
                return BadRequest("User already exists");

            model.Role = "User"; 
            UserStore.AddUser(model);

            return Ok("User registered successfully");
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(UserModel model)
        {
            var user = UserStore.ValidateUser(model.Username, model.Password);

            if (user == null)
                return Unauthorized();

            var token = JwtTokenHelper.GenerateToken(user);

            return Ok(new
            {
                token = token,
                role = user.Role
            });
        }
    }
}
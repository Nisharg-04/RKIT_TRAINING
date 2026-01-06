using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthDemo.Services
{
    using AuthDemo.App_Start;
    using AuthDemo.Models;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public static class JwtTokenHelper
    {
        public static string GenerateToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtConfig.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var token = new JwtSecurityToken(
                issuer: JwtConfig.Issuer,
                audience: JwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
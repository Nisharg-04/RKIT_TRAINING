using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.Text;
using ExpenseTracker.App_Start;

namespace ExpenseTracker.Common
{
    public static class JwtHelper
    {
        public static string GenerateToken(int userId,string name)
        {
            var key = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(JwtConfig.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("userId", userId.ToString()),
            new Claim("name", name)
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
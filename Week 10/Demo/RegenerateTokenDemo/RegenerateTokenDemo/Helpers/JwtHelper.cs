using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using RegenerateTokenDemo.Models;
using System;

namespace RegenerateTokenDemo.Helpers
{
   

    public static class JwtHelper
    {
        private static readonly string Secret = "SUPER_SECRET_KEY_1234561111111111111";

        public static string GenerateAccessToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var token = new JwtSecurityToken(
                issuer: "AuthDemo",
                audience: "AuthDemo",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
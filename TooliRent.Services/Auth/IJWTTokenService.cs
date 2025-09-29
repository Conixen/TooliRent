using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TooliRent.Models;

namespace TooliRent.Services.Auth
{
    public interface IJWTTokenService
    {
        string GenerateToken(User user, IEnumerable<string>? roles = null);
    }
    public class JWTTokenService : IJWTTokenService
    {
        private readonly IConfiguration _cfg;
        public JWTTokenService(IConfiguration cfg)
        {
            _cfg = cfg;
        }
        public string GenerateToken(User user, IEnumerable<string>? roles = null)
        {
            var jwt = _cfg.GetSection("Jwt");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            //if (roles is not null)
            //{
            //    claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            //    claims.Add(new Claim(ClaimTypes.Role, user.Role));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                    issuer: jwt["Issuer"],
                    audience: jwt["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

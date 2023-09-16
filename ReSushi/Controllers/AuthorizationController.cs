using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReSushi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReSushi.Controllers
{
    public class AuthorizationController
    {
        private readonly IConfiguration _configuration;

        private AuthorizationResponse GenerateAuthorizationToken(string userId, string userName)
        {
            var now = DateTime.UtcNow;
            var secret = _configuration.GetValue<string>("Secret");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var userClaims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
        new Claim(ClaimTypes.NameIdentifier, userId),
    };

            //userClaims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var expires = now.Add(TimeSpan.FromMinutes(60));

            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: userClaims,
                    expires: expires,
                    audience: "https://localhost:7051/",
                    issuer: "https://localhost:7051/",
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            //we don't know about thread safety of token handler

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var resp = new AuthorizationResponse
            {
                UserId = userId,
                AuthorizationToken = encodedJwt,
                RefreshToken = string.Empty
            };

            return resp;
        }
    }
}

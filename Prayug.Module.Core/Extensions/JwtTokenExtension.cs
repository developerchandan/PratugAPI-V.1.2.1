using Microsoft.IdentityModel.Tokens;
using Prayug.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prayug.Module.Core.Extensions
{
    public static class JwtTokenExtension
    {
        public static (string, DateTime) CreateToken(this IEnumerable<Claim> authClaims)
        {

            return GenerateToken(authClaims);
        }

        private static (string, DateTime) GenerateToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalSettings.ApiKey));

            var token = new JwtSecurityToken(
                issuer: GlobalSettings.Issuer,
                audience: GlobalSettings.Audience,
                expires: DateTime.Now.AddMinutes(120),
                claims: authClaims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }
    }
}

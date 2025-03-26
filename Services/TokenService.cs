
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjectApi.Interfaces;

namespace ProjectApi.Services
{
    public class TokenService : ITokenService
    {
        private SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gyqew62AF65KJ968DCGVCHFVIRFI16GfdefjdTYI"));
        private string issuer = "https://newborn.com";

        public SecurityToken GetToken(List<Claim> claims) =>
            new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

        public TokenValidationParameters GetTokenValidationParameters() =>
            new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

        public string WriteToken(SecurityToken token) =>
            new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static class TokenServiceHelper
    {
        public static void AddTokenService(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
        }
    }

}

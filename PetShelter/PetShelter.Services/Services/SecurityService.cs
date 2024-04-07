using Microsoft.IdentityModel.Tokens;
using PetShelter.Shared.Attributes;
using PetShelter.Shared.Security.Contracts;
using PetShelter.Shared.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetShelter.Services.Services
{
    [AutoBind]
    public class SecurityService : ISecurityService
    {
        private readonly IJwtSettings _settings;

        public SecurityService(IJwtSettings settings)
        {
            _settings = settings;
        }

        public string GenerateJwtToken(string username, string role = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Aud, _settings.Audience),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())
            };

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToLower()));
            }

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_settings.ExpirationInMinutes)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}

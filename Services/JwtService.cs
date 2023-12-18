using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalWebsiteApi.Services
{
    public class JwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationInMinutes;

        public JwtService(string key, string issuer, string audience, int expirationInMinutes)
        {
            _key = key;
            _issuer = issuer;
            _audience = audience;
            _expirationInMinutes = expirationInMinutes;
        }


        public string GenerateJwtToken(string username, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()) 
        }),
                Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

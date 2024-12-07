using GenericApi.DataObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GenericApi.Infrastructure
{
    public sealed class TokenProvider(IConfiguration configuration)
    {
        public string Create(User user)
        {
            string secretKey = configuration["Jwt:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("role", "user")
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}

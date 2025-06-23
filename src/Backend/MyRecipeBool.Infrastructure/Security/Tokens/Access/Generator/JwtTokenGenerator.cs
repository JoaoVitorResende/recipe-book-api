using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler,IAccessTokenGenerator
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _singingKey;
        public JwtTokenGenerator(uint expirationTimeMinutes, string singingKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _singingKey = singingKey;
        }
        public string Generate(Guid userIdentifier)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userIdentifier.ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_singingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}

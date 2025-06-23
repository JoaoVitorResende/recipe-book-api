using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access.Validator
{
    public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
    {
        private readonly string _singingKey;
        public JwtTokenValidator(string signingKey)
        {
            _singingKey = signingKey;
        }
        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey(_singingKey),
                ClockSkew = new TimeSpan(0)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, validationParameters,out _);
            var userIdentifier = principal.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
            return Guid.Parse(userIdentifier);
        }
    }
}

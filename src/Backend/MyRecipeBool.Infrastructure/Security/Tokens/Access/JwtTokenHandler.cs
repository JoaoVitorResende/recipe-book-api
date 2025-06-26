using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string _singingKey)
        {
            var bytes = Encoding.UTF8.GetBytes(_singingKey);
            return new SymmetricSecurityKey(bytes);
        }
    }
}

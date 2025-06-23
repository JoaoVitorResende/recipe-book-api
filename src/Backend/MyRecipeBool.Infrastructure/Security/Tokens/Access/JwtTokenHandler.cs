using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string _singingKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_singingKey));
        }
    }
}

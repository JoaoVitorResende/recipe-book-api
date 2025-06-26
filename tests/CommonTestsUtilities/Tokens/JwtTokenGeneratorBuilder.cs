using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator;
namespace CommonTestsUtilities.Tokens
{
    public class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator build() => new JwtTokenGenerator(expirationTimeMinutes: 5, singingKey: "tttttttttttttttttttttttttttttttt");
    }
}

using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.API.Token
{
    public class httpContextTokenValue : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public httpContextTokenValue(IHttpContextAccessor contextAccessor)
        {
             _contextAccessor = contextAccessor;
        }
        public string Value()
        {
            var authentication = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
            return authentication["Bearer ".Length..].Trim();
        }
    }
}

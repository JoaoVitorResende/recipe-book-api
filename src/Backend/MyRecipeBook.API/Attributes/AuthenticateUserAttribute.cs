using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.API.Filters;

namespace MyRecipeBook.API.Attributes
{
    public class AuthenticateUserAttribute : TypeFilterAttribute
    {
        public AuthenticateUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}

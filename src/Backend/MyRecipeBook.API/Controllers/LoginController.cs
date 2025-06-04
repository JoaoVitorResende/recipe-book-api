using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers
{
    public class LoginController : MyRecipeBookBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var result = await useCase.Execute(request);
            return Ok(result);
        }
    }
}

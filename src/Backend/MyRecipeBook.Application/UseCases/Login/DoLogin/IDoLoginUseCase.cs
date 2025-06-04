using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegistredUserJson> Execute(RequestLoginJson requestLoginJson);
    }
}

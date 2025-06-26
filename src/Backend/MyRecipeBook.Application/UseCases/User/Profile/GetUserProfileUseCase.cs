using AutoMapper;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Services.LoggedUser;

namespace MyRecipeBook.Application.UseCases.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly IloggedUser _loggedUser;
        private readonly IMapper _mapper;
        public GetUserProfileUseCase(IloggedUser loggedUser, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
        }
        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loggedUser.User();
            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}

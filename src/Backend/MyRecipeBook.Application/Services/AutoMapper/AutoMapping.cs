using AutoMapper;
using MyRecipeBook.Communication.Enuns;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using Sqids;

namespace MyRecipeBook.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        private readonly SqidsEncoder<long> _idEncoder;
        public AutoMapping(SqidsEncoder<long> idEnconder)
        {
            _idEncoder = idEnconder;
            DomainToResponse();
            RequestToDomain();
        }
        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(destine => destine.Password, except => except.Ignore());
            CreateMap<RequestRecipeJson, Domain.Entities.Recipe>()
                .ForMember(destine => destine.Instructions, except => except.Ignore())
                .ForMember(destine => destine.Ingredients, except => except.MapFrom(source => source.Ingredients.Distinct()))
                .ForMember(destine => destine.DishTypes, except => except.MapFrom(source => source.DishTypes.Distinct()));
            CreateMap<string, Domain.Entities.Ingredient>()
                .ForMember(destine => destine.Item, opt => opt.MapFrom(source => source));
            CreateMap<DishType, Domain.Entities.DishType>()
               .ForMember(destine => destine.Type, opt => opt.MapFrom(source => source));
            CreateMap<RequestInstructionJson, Domain.Entities.Instruction>();
        }
        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
            CreateMap<Domain.Entities.Recipe, ResponseRegistredRecipeJson>()
                .ForMember(destine => destine.Id, config => config.MapFrom(source => _idEncoder.Encode(source.Id)));
        }
    }
}
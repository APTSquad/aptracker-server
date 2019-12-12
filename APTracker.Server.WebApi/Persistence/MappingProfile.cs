using APTracker.Server.WebApi.Dto.Bag;
using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels.Commands.Bag;
using APTracker.Server.WebApi.ViewModels.Commands.Bag.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Bag.GetAll;
using AutoMapper;

namespace APTracker.Server.WebApi.Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bag, BagGetAllResponse>();
            CreateMap<User, UserSimplifiedView>();


            CreateMap<BagCreateRequest, Bag>();
            // .ForMember(x => x.ResponsibleId, opt => opt.MapFrom(dto => dto.ResponsibleId));
        }
    }
}
using APTracker.Server.WebApi.Dto.Bag;
using APTracker.Server.WebApi.Persistence.Entities;
using AutoMapper;

namespace APTracker.Server.WebApi.Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bag, BagSimplifiedDto>();
            CreateMap<User, UserSimplifiedDto>();


            CreateMap<BagCreateDto, Bag>();
            // .ForMember(x => x.ResponsibleId, opt => opt.MapFrom(dto => dto.ResponsibleId));
        }
    }
}
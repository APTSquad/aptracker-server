using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels.Commands.Bag.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Bag.GetAll;
using APTracker.Server.WebApi.ViewModels.Commands.Client.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.ViewModels.Commands.Project.Create;
using APTracker.Server.WebApi.ViewModels.Commands.User.Modify;
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
            CreateMap<Client, ClientView>();
            CreateMap<ConsumptionArticle, ArticleView>();
            CreateMap<Project, ProjectView>();
            CreateMap<Bag, BagView>();

            CreateMap<Client, ClientCreateResponse>();
            CreateMap<ClientCreateCommand, Client>();

            CreateMap<ProjectCreateCommand, Project>();
            CreateMap<Project, ProjectCreateResponse>();
            // .ForMember(x => x.ResponsibleId, opt => opt.MapFrom(dto => dto.ResponsibleId));
        }
    }
}
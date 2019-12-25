using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels.Commands.Articles.GetAll;
using APTracker.Server.WebApi.ViewModels.Commands.Bag.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Bag.GetAll;
using APTracker.Server.WebApi.ViewModels.Commands.Bag.GetById;
using APTracker.Server.WebApi.ViewModels.Commands.Client.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.ViewModels.Commands.Project.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Project.GetAll;
using APTracker.Server.WebApi.ViewModels.Commands.User;
using APTracker.Server.WebApi.ViewModels.Commands.User.Modify;
using AutoMapper;

namespace APTracker.Server.WebApi.Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bag, BagGetAllResponse>();
            CreateMap<Bag, BagGetByIdResponse>();
            
            
            CreateMap<Project, ProjectGetAllResponse>();
            CreateMap<ConsumptionArticle, ArticleGetAllResponse>();
            CreateMap<User, UserSimplifiedView>();
            CreateMap<ConsumptionArticle, BagArticleView>();
            CreateMap<Project, BagProjectView>();
            CreateMap<Client, BagClientView>();


            CreateMap<BagCreateRequest, Bag>();
            CreateMap<Client, ClientView>();
            CreateMap<ConsumptionArticle, ArticleView>();
            CreateMap<Project, ProjectView>();
            CreateMap<Bag, BagView>();

            CreateMap<Client, ClientCreateResponse>();
            CreateMap<ClientCreateRequest, Client>();

            CreateMap<ProjectCreateRequest, Project>();
            CreateMap<Project, ProjectCreateResponse>();
        }
    }
}
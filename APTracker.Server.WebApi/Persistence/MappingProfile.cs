using APTracker.Server.WebApi.Commands.Articles;
using APTracker.Server.WebApi.Commands.Articles.Create;
using APTracker.Server.WebApi.Commands.Articles.GetAll;
using APTracker.Server.WebApi.Commands.Bag.Create;
using APTracker.Server.WebApi.Commands.Bag.GetAll;
using APTracker.Server.WebApi.Commands.Bag.GetById;
using APTracker.Server.WebApi.Commands.Client.Create;
using APTracker.Server.WebApi.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.Commands.Project.Create;
using APTracker.Server.WebApi.Commands.Project.GetAll;
using APTracker.Server.WebApi.Commands.Report;
using APTracker.Server.WebApi.Commands.User;
using APTracker.Server.WebApi.Persistence.Entities;
using AutoMapper;

namespace APTracker.Server.WebApi.Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bag, BagGetAllResponse>();
            CreateMap<Bag, BagGetByIdResponse>();


            CreateMap<ArticleCreateRequest, ConsumptionArticle>();
            CreateMap<ConsumptionArticle, ArticleDetailResponse>();
            CreateMap<Project, ArticleDetailResponse.ProjectSimplified>();
            CreateMap<Client, ArticleDetailResponse.ClientSimplified>();

            CreateMap<ConsumptionArticle, ReportArticleItem>();
            CreateMap<Project, ReportProjectItem>();
            CreateMap<Client, ReportClientItem>();


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
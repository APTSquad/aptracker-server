using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views;

namespace APTracker.Server.WebApi.ViewModels.Commands.Articles.GetAll
{
    public class ArticleGetAllResponse : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public BagView? Bag { get; set; }
    }
}
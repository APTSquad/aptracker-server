using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Articles.Modify
{
    public class ArticleModifyRequest : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
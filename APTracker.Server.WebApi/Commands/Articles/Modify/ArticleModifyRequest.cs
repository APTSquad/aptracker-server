using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Articles.Modify
{
    public class ArticleModifyRequest : IEntity
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Hierarchy.Views
{
    public class ArticleView : IEntity
    {
        public string Name { get; set; }
        public BagView Bag { get; set; }
        public long Id { get; set; }
    }
}
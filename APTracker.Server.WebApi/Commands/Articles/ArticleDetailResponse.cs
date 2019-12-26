using APTracker.Server.WebApi.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Articles
{
    public class ArticleDetailResponse : IEntity
    {
        public string Name { get; set; }
        public ProjectSimplified Project { get; set; }
        public long Id { get; set; }

        public class ProjectSimplified
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public BagView? Bag { get; set; }
            public ClientSimplified? Client { get; set; }
        }

        public class ClientSimplified
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public BagView? Bag { get; set; }
        }
    }
}
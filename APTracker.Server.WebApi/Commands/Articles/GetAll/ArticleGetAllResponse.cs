using APTracker.Server.WebApi.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Articles.GetAll
{
    public class ArticleGetAllResponse : IEntity
    {
        public string Name { get; set; }
        public BagView? Bag { get; set; }
        public bool IsActive { get; set; }
        public bool IsCommon { get; set; }
        public long Id { get; set; }
    }
}
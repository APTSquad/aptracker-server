using System.Collections.Generic;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views
{
    public class ProjectView : IEntity
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public BagView Bag { get; set; }
        
        public ICollection<ArticleView> Articles { get; set; }
    }
}
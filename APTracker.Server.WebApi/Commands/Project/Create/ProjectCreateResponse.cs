using APTracker.Server.WebApi.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Project.Create
{
    public class ProjectCreateResponse : IEntity
    {
        public string Name { get; set; }

        public BagView? Bag { get; set; }
        public long Id { get; set; }
    }
}
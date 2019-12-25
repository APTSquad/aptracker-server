using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Project.Create
{
    public class ProjectCreateResponse : IEntity
    {
        public string Name { get; set; }

        public Persistence.Entities.Bag? Bag { get; set; }

        public long Id { get; set; }
    }
}
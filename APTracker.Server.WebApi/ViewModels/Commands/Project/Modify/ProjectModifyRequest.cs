using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Project.Modify
{
    public class ProjectModifyRequest : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
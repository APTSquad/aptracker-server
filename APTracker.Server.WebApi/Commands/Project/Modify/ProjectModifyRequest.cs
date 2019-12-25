using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Project.Modify
{
    public class ProjectModifyRequest : IEntity
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}
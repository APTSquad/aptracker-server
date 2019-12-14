using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Project.Modify
{
    public class ProjectModifyCommand : IEntity
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}
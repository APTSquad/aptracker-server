using System.ComponentModel.DataAnnotations;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Project.Create
{
    public class ProjectCreateResponse : IEntity
    {
        [Required] public string Name { get; set; }

        public long Id { get; set; }
    }
}
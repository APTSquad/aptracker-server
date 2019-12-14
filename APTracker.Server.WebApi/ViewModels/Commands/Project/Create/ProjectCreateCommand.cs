using System.ComponentModel.DataAnnotations;

namespace APTracker.Server.WebApi.ViewModels.Commands.Project.Create
{
    public class ProjectCreateCommand
    {
        [Required] public string Name { get; set; }

        [Required] public long ClientId { get; set; }
    }
}
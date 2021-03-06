using System.ComponentModel.DataAnnotations;

namespace APTracker.Server.WebApi.Commands.Project.Create
{
    public class ProjectCreateRequest
    {
        [Required] public string Name { get; set; }

        [Required] public long ClientId { get; set; }
    }
}
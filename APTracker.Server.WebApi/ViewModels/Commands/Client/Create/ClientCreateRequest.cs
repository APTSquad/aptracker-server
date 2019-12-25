using System.ComponentModel.DataAnnotations;

namespace APTracker.Server.WebApi.ViewModels.Commands.Client.Create
{
    public class ClientCreateRequest
    {
        [Required] public string Name { get; set; }

        [Required] public long BagId { get; set; }
    }
}
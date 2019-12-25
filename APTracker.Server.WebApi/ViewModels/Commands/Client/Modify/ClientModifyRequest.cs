using System.ComponentModel.DataAnnotations;

namespace APTracker.Server.WebApi.ViewModels.Commands.Client.Modify
{
    public class ClientModifyRequest
    {
        public long Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public long BagId { get; set; }
    }
}
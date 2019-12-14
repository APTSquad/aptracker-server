using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views;

namespace APTracker.Server.WebApi.ViewModels.Commands.Client.Create
{
    public class ClientCreateResponse : IEntity
    {
        public string Name { get; set; }

        public BagView Bag { get; set; }
        public long Id { get; set; }
    }
}
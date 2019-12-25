using APTracker.Server.WebApi.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Client.Create
{
    public class ClientCreateResponse : IEntity
    {
        public string Name { get; set; }

        public BagView Bag { get; set; }
        public long Id { get; set; }
    }
}
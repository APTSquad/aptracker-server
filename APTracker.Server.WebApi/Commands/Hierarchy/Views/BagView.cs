using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Hierarchy.Views
{
    public class BagView : IEntity
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}
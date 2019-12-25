using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Bag.GetById
{
    public class BagProjectView : IEntity
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}
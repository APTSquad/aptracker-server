using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Bag.GetById
{
    public class BagProjectView : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
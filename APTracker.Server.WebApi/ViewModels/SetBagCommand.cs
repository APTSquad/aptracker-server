using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels
{
    public class SetBagCommand : IEntity
    {
        public long Id { get; set; }
        public long? BagId { get; set; }
    }
}
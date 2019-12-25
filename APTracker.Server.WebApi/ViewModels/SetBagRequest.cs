using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels
{
    public class SetBagRequest : IEntity
    {
        public long? BagId { get; set; }
        public long Id { get; set; }
    }
}
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Bag.Modify
{
    public class BagModifyRequest : IEntity
    {
        public string Name { get; set; }
        public long? ResponsibleId { get; set; }
        public long Id { get; set; }
    }
}
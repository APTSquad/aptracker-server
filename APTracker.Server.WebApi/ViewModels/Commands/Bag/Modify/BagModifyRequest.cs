using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Bag.Modify
{
    public class BagModifyRequest : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public long? ResponsibleId { get; set; }
        
    }
}
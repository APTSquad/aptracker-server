namespace APTracker.Server.WebApi.ViewModels.Commands.Bag.Create
{
    public class BagCreateRequest
    {
        public string Name { get; set; }
        public long? ResponsibleId { get; set; }
    }
}
namespace APTracker.Server.WebApi.Commands.Transfer
{
    public class TransferRequest
    {
        public long ItemId { get; set; }
        public long DestinationId { get; set; }
    }
}
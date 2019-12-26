namespace APTracker.Server.WebApi.Commands.Bag.GetById
{
    public class BagArticleView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public BagProjectView Project { get; set; }
    }
}
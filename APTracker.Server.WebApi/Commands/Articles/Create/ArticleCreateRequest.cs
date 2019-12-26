namespace APTracker.Server.WebApi.Commands.Articles.Create
{
    public class ArticleCreateRequest
    {
        public string Name { get; set; }
        public long? ProjectId { get; set; }
    }
}
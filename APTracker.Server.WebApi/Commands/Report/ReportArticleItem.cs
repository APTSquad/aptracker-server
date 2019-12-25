using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Report
{
    public class ReportArticleItem : IEntity
    {
        public string Name { get; set; }

        public bool IsChecked { get; set; }
        public long Id { get; set; }
    }
}
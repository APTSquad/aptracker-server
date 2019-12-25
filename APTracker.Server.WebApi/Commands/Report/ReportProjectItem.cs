using System.Collections.Generic;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Report
{
    public class ReportProjectItem : IEntity
    {
        public string Name { get; set; }
        public ICollection<ReportArticleItem> Articles { get; set; }
        public bool IsChecked { get; set; }
        public long Id { get; set; }
    }
}
using System.Collections.Generic;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Report
{
    public class ReportClientItem : IEntity
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public ICollection<ReportProjectItem> Projects { get; set; }
        public long Id { get; set; }
    }
}
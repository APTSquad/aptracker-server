using System;
using System.Collections.Generic;

namespace APTracker.Server.WebApi.Commands.Report
{
    public class ReportConsumptionItem
    {
        public long ArticleId { get; set; }
        public double HoursConsumption { get; set; }
    }
    public class SaveReportCommand
    {
        public DateTime Date { get; set; }
        public long UserId { get; set; }
        public ICollection<ReportConsumptionItem> Articles { get; set; }
    }
}
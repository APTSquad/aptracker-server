using System;
using System.Collections.Generic;
using APTracker.Server.WebApi.Controllers;

namespace APTracker.Server.WebApi.Commands.Report
{
    public class ReportConsumptionItem
    {
        public long ArticleId { get; set; }
        public double HoursConsumption { get; set; }
    }

    public class ReportSaveCommand
    {
        public DateTime Date { get; set; }
        public long UserId { get; set; }

        public ReportController.ReportState ReportState { get; set; }
        public ICollection<ReportConsumptionItem> Articles { get; set; }
    }
}
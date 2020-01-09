using System;

namespace APTracker.Server.WebApi.Commands.Report
{
    public class ReportTemplateRequest
    {
        public long UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
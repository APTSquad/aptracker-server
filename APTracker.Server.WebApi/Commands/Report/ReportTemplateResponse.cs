using System.Collections.Generic;

namespace APTracker.Server.WebApi.Commands.Report
{
    public class ReportTemplateResponse
    {
        public ICollection<ReportArticleItem> CommonArticles { get; set; }
        public ICollection<ReportClientItem> Clients { get; set; }
    }
}
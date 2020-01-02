using System;

namespace APTracker.Server.WebApi.Commands.BagReport
{
    public class BagReportRequest
    {
        public long BagId { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
    }
}
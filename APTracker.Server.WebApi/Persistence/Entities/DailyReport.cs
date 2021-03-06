using System;
using System.Collections.Generic;

namespace APTracker.Server.WebApi.Persistence.Entities
{
    /// <summary>
    ///     Отчет за день
    /// </summary>
    public class DailyReport : IEntity
    {
        /// <summary>
        ///     Элементы дневного отчета
        /// </summary>
        public ICollection<ConsumptionReportItem> ReportItems { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public long UserId { get; set; }

        public ReportState State { get; set; } = ReportState.Editable;


        /// <summary>
        ///     Время сохранения отчета
        /// </summary>
        public DateTime LastEdited { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
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

        /// <summary>
        ///     Дата отчета
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Время сохранения отчета
        /// </summary>
        public DateTime Saved { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
namespace APTracker.Server.WebApi.Persistence.Entities
{
    /// <summary>
    ///     Затрата на статью
    /// </summary>
    public class ConsumptionReportItem : IEntity
    {
        /// <summary>
        ///     Затраты часов
        /// </summary>
        public double HoursConsumption { get; set; }

        /// <summary>
        ///     Статья
        /// </summary>
        public ConsumptionArticle Article { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
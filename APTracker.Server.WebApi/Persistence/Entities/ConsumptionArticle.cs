namespace APTracker.Server.WebApi.Persistence.Entities
{
    /// <summary>
    ///     Статья расхода времени
    /// </summary>
    public class ConsumptionArticle : IEntity
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Признак активности статьи
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Индикатор общности статьи
        /// </summary>
        public bool IsCommon { get; set; }

        /// <summary>
        ///     Проект статьи
        /// </summary>
        public Project Project { get; set; }

        public long? ProjectId { get; set; }

        /// <summary>
        ///     Портфель статьи
        ///     Необязательный
        /// </summary>
        public Bag? Bag { get; set; }

        public long? BagId { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
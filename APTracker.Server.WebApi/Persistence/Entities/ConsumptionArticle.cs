namespace APTracker.Server.WebApi.Persistence.Entities
{
    /// <summary>
    ///     Статья расхода времени
    /// </summary>
    public class ConsumptionArticle : IEntity
    {
        /// <summary>
        ///     Признак активности статьи
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Проект статьи
        /// </summary>
        public Project Project { get; set; }

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
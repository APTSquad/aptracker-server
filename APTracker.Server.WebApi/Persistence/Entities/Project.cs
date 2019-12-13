using System.Collections.Generic;

namespace APTracker.Server.WebApi.Persistence.Entities
{
    /// <summary>
    ///     Проект
    /// </summary>
    public class Project : IEntity
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Клиент проекта
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        ///     Портфель проекта
        ///     Необязательный
        /// </summary>
        public Bag? Bag { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }
        
        public ICollection<ConsumptionArticle> Articles { get; set; }
    }
}
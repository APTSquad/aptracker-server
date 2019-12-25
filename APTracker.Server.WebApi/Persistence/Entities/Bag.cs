using System.Collections.Generic;

namespace APTracker.Server.WebApi.Persistence.Entities
{
    /// <summary>
    ///     Портфель
    /// </summary>
    public class Bag : IEntity
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Проекты
        /// </summary>
        public ICollection<Project> Projects { get; set; }

        /// <summary>
        ///     Клиенты
        /// </summary>
        public ICollection<Client> Clients { get; set; }

        /// <summary>
        ///     Статьи
        /// </summary>
        public ICollection<ConsumptionArticle> Articles { get; set; }

        /// <summary>
        ///     Ответственный
        /// </summary>
        public User? Responsible { get; set; }

        /// <summary>
        ///     Идентификатор ответственного
        /// </summary>
        public long? ResponsibleId { get; set; } = null;

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
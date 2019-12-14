using System.Collections.Generic;

namespace APTracker.Server.WebApi.Persistence.Entities
{
    /// <summary>
    ///     Клиент
    /// </summary>
    public class Client : IEntity
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Проекты клиента
        /// </summary>
        public ICollection<Project> Projects { get; set; }

        /// <summary>
        ///     Портфель клиента
        /// </summary>
        public Bag Bag { get; set; }

        /// <summary>
        ///     Идентификатор портфеля
        /// </summary>
        public long BagId { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
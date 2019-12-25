using System.Collections.Generic;
using APTracker.Server.WebApi.Commands.User;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.Commands.Bag.GetById
{
    public class BagGetByIdResponse : IEntity
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Ответственный
        /// </summary>
        public UserSimplifiedView Responsible { get; set; }

        /// Проекты
        /// </summary>
        public ICollection<BagProjectView> Projects { get; set; }

        /// <summary>
        ///     Клиенты
        /// </summary>
        public ICollection<BagClientView> Clients { get; set; }

        /// <summary>
        ///     Статьи
        /// </summary>
        public ICollection<BagArticleView> Articles { get; set; }

        public long Id { get; set; }
    }
}
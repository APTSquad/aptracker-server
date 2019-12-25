using APTracker.Server.WebApi.Commands.User;

namespace APTracker.Server.WebApi.Commands.Bag.Create
{
    public class BagCreateResponse
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Ответственный
        /// </summary>
        public UserSimplifiedView Responsible { get; set; }
    }
}
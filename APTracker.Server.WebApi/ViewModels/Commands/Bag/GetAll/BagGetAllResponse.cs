using APTracker.Server.WebApi.ViewModels.Commands.User.Modify;

namespace APTracker.Server.WebApi.ViewModels.Commands.Bag.GetAll
{
    public class BagGetAllResponse
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
using APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views;

namespace APTracker.Server.WebApi.ViewModels.Commands.Project.GetAll
{
    public class ProjectGetAllResponse
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
        ///     Портфель
        /// </summary>
        public BagView? Bag { get; set; }
    }
}
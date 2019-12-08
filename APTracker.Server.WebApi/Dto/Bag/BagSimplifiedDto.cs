namespace APTracker.Server.WebApi.Dto.Bag
{
    public class BagSimplifiedDto
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
        public UserSimplifiedDto Responsible { get; set; }
    }
}
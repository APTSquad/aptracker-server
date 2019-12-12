using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.User
{
    public class UserModifyCommand : IEntity
    {
        public string Name { get; set; }

        public double Rate { get; set; } = 1.0;

        public long Id { get; set; }
    }
}
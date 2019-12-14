using System.Collections.Generic;
using APTracker.Server.WebApi.Persistence.Entities;

namespace APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views
{
    public class ClientView : IEntity
    {
        public string Name { get; set; }

        public BagView Bag { get; set; }

        public ICollection<ProjectView> Projects { get; set; }
        public long Id { get; set; }
    }
}
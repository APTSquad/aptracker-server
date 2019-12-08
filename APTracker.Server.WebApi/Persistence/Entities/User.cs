using System.Collections.Generic;

namespace APTracker.Server.WebApi.Persistence.Entities
{
    public class User : IEntity
    {
        public string Name { get; set; }
        public Role Role { get; set; } = Role.Developer;
        public string Email { get; set; }

        public double Rate { get; set; } = 1.0;

        public ICollection<Bag> Bags { get; set; }
        public long Id { get; set; }
    }
}
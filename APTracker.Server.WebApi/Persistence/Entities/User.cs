namespace APTracker.Server.WebApi.Persistence.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; } = Role.Developer;
        public string Email { get; set; }
        public double Rate { get; set; } = 1.0;
    }
}
using APTracker.Server.WebApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
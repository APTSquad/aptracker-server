using APTracker.Server.WebApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bag> Bags { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ConsumptionArticle> ConsumptionArticles { get; set; }
        public DbSet<ConsumptionReportItem> ConsumptionReportItems { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
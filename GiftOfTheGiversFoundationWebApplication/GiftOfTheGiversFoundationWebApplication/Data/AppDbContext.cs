using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversFoundationWebApplication.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<GiftOfTheGiversFoundationWebApplication.Models.User> Users { get; set; }
        public DbSet<GiftOfTheGiversFoundationWebApplication.Models.Volunteer> Volunteers { get; set; }
        public DbSet<GiftOfTheGiversFoundationWebApplication.Models.Donation> Donations { get; set; }
        public DbSet<GiftOfTheGiversFoundationWebApplication.Models.IncidentAlert> IncidentAlerts { get; set; }
        public DbSet<GiftOfTheGiversFoundationWebApplication.Models.ReliefEffort> ReliefEfforts { get; set; }

        
    }
}

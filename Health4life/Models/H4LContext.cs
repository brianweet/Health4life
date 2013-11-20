using System.Data.Entity;

namespace Health4life.Models
{
    public class H4LContext : DbContext
    {
        public H4LContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ShareKey> ShareKeys { get; set; }
    }
}
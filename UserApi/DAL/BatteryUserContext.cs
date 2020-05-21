using UserApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UserApi.DAL
{
    public class BatteryUserContext : DbContext
    {
        public BatteryUserContext(DbContextOptions<BatteryUserContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        // Override Pluralisation of Feedback Model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}

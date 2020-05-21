using BatteryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BatteryApi.DAL
{
    public class BatteryLifetimeContext : DbContext
    {
        public BatteryLifetimeContext(DbContextOptions<BatteryLifetimeContext> options) : base(options)
        {

        }

        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Batch> Batches { get; set; }

        // Override Pluralisation of Feedback Model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Battery>().ToTable("Battery");
            modelBuilder.Entity<Batch>().ToTable("Batch");
        }
    }
}

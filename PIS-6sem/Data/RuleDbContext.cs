using Microsoft.EntityFrameworkCore;
using PIS_6sem.Entities;


namespace PIS_6sem.Data
{
    public class RuleDbContext : DbContext
    {
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileProperty> ProfileProperties { get; set; }
        public DbSet<Guidance> Guidances { get; set; }
        public DbSet<TargetDocument> TargetDocuments { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=rules.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Rule -> Profiles (один ко многим)
            modelBuilder.Entity<Rule>()
                .HasMany(r => r.Profiles)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Rule -> TargetDocuments (один ко многим)
            modelBuilder.Entity<Rule>()
                .HasMany(r => r.TargetDocuments)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Rule -> Guidance (один к одному)
            modelBuilder.Entity<Rule>()
                .HasOne(r => r.Guidance)
                .WithOne()
                .HasForeignKey<Guidance>("RuleId")
                .OnDelete(DeleteBehavior.Cascade);

            // Profile -> ProfileProperties (один ко многим)
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.Properties)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Guidance -> Organizations (один ко многим)
            modelBuilder.Entity<Guidance>()
                .HasMany(g => g.Organizations)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

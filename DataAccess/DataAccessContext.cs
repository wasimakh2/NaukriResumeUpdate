using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataAccessContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NaukriJobDetail>()
                .HasIndex(u => u.DataJobId)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=NaukriResume.db");
        }

        public DbSet<NaukriJobDetail> NaukriJobDetails { get; set; }
    }
}
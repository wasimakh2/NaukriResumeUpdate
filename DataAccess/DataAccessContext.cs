using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataAccessContext : DbContext
    {



        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite("Data Source=NaukriResume.db");



        public DbSet<NaukriJobDetail> NaukriJobDetails { get; set; }
    }
}

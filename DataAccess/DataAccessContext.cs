using System.Data.Entity;

namespace DataAccess
{
    public class DataAccessContext : DbContext
    {

        public DataAccessContext(): base("DataAccessContext")
        {

        }

        public DbSet<NaukriJobDetail> NaukriJobDetails { get; set; }
    }
}

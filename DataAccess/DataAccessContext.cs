using DataAccessLayer.Entity;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class DataAccessContext : DbContext
    {

        public DataAccessContext(): base("DataAccessContext")
        {

        }

        public DbSet<NaukriJobDetail> NaukriJobDetails { get; set; }
    }
}

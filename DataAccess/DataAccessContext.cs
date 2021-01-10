using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

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

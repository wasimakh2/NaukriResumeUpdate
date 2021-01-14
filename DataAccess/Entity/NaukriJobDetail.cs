using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entity
{
    public class NaukriJobDetail : IJobDetail
    {
        public int NaukriJobDetailId { get; set; }

        public String Title { get; set; }

        public String Company { get; set; }

        public String Ratings { get; set; }
        public String Reviews { get; set; }
        public String Experience { get; set; }

        public String Salary { get; set; }
        public String Location { get; set; }
        public String Job_Post_History { get; set; }
        public String URL { get; set; }




    }
}

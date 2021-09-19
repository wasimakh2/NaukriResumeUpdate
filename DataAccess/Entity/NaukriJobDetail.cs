using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class NaukriJobDetail : IJobDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Company { get; set; }
        public string Experience { get; set; }
        public string Salary { get; set; }
        public string Location { get; set; }

        [Required]
        [StringLength(500)]
        public string URL { get; set; }

        [Required]
        public string DataJobId { get; set; }

        public string Ratings { get; set; }
        public string Reviews { get; set; }
        public string Job_Post_History { get; set; }

        public bool AppliedStatus { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
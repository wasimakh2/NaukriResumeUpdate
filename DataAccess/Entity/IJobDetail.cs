namespace DataAccessLayer.Entity
{
    public interface IJobDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Company { get; set; }
        public string Experience { get; set; }
        public string Salary { get; set; }
        public string Location { get; set; }
        public string URL { get; set; }
    }
}
namespace DataAccessLayer.Entity
{
    public interface IJobDetail
    {
        string Company { get; set; }
        string Experience { get; set; }
        string Job_Post_History { get; set; }
        string Location { get; set; }
        int Id { get; set; }
        string Ratings { get; set; }
        string Reviews { get; set; }
        string Salary { get; set; }
        string Title { get; set; }
        string URL { get; set; }
    }
}
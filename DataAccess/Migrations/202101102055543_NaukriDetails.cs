namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NaukriDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NaukriJobDetails",
                c => new
                    {
                        NaukriJobDetailId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Company = c.String(),
                        Ratings = c.String(),
                        Reviews = c.String(),
                        Experience = c.String(),
                        Salary = c.String(),
                        Location = c.String(),
                        Job_Post_History = c.String(),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.NaukriJobDetailId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NaukriJobDetails");
        }
    }
}

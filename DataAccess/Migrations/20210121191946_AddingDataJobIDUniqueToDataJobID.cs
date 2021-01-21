using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddingDataJobIDUniqueToDataJobID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NaukriJobDetails_URL",
                table: "NaukriJobDetails");

            migrationBuilder.CreateIndex(
                name: "IX_NaukriJobDetails_DataJobId",
                table: "NaukriJobDetails",
                column: "DataJobId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NaukriJobDetails_DataJobId",
                table: "NaukriJobDetails");

            migrationBuilder.CreateIndex(
                name: "IX_NaukriJobDetails_URL",
                table: "NaukriJobDetails",
                column: "URL",
                unique: true);
        }
    }
}

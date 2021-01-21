using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddingDataJobID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataJobId",
                table: "NaukriJobDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NaukriJobDetails_URL",
                table: "NaukriJobDetails",
                column: "URL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NaukriJobDetails_URL",
                table: "NaukriJobDetails");

            migrationBuilder.DropColumn(
                name: "DataJobId",
                table: "NaukriJobDetails");
        }
    }
}

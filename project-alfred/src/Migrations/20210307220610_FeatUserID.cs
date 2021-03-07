using Microsoft.EntityFrameworkCore.Migrations;

namespace project_alfred.Migrations
{
    public partial class FeatUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "user",
                table: "SongRecords",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user",
                table: "SongRecords");
        }
    }
}

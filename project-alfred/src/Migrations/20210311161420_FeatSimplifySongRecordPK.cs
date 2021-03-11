using Microsoft.EntityFrameworkCore.Migrations;

namespace project_alfred.Migrations
{
    public partial class FeatSimplifySongRecordPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords",
                columns: new[] { "user", "url" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords",
                columns: new[] { "user", "publishedAt", "url" });
        }
    }
}

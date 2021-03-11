using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace project_alfred.Migrations
{
    public partial class FeatSongRecordNewPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SongRecords");

            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "SongRecords",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "publishedAt",
                table: "SongRecords",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords",
                columns: new[] { "user", "publishedAt", "url" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords");

            migrationBuilder.DropColumn(
                name: "publishedAt",
                table: "SongRecords");

            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "SongRecords",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "SongRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongRecords",
                table: "SongRecords",
                column: "ID");
        }
    }
}

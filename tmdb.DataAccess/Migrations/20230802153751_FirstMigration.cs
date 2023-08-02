using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tmdb.DataAccess.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    IsWatched = table.Column<bool>(type: "bit", nullable: false),
                    MovieRating = table.Column<float>(type: "real", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 8, 2, 15, 37, 51, 434, DateTimeKind.Utc).AddTicks(2328))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_UserId_MovieId",
                table: "WatchLists",
                columns: new[] { "UserId", "MovieId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchLists");
        }
    }
}

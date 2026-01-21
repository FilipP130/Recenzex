using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recenzex.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFilmRatingCache : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatingSum",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "RatingSum",
                table: "Films");
        }
    }
}

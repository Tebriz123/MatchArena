using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchArena.Persistence.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRatingInTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Tournaments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Tournaments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

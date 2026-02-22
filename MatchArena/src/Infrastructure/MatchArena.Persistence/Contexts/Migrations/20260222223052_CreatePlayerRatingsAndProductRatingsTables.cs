using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchArena.Persistence.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlayerRatingsAndProductRatingsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_AspNetUsers_CaptainId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Payments_PaymentId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Teams_TeamId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId",
                table: "TournamentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrations_CaptainId",
                table: "TournamentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrations_TournamentId",
                table: "TournamentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TeamInvites_TeamId",
                table: "TeamInvites");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FieldId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CaptainId",
                table: "TournamentRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "CaptainUserId",
                table: "TournamentRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "TournamentId1",
                table: "TournamentRegistrations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FieldId1",
                table: "Reservations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "ProductImage",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "ProductId1",
                table: "ProductImage",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Payments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Payments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PlayerRatings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    RaterPlayerId = table.Column<long>(type: "bigint", nullable: false),
                    RatedPlayerId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRatings_Players_RatedPlayerId",
                        column: x => x.RatedPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerRatings_Players_RaterPlayerId",
                        column: x => x.RaterPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRatings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_CaptainUserId",
                table: "TournamentRegistrations",
                column: "CaptainUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_TournamentId_TeamId",
                table: "TournamentRegistrations",
                columns: new[] { "TournamentId", "TeamId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_TournamentId1",
                table: "TournamentRegistrations",
                column: "TournamentId1");

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvites_TeamId_PlayerId",
                table: "TeamInvites",
                columns: new[] { "TeamId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FieldId_ReservedDate_ReservedTime",
                table: "Reservations",
                columns: new[] { "FieldId", "ReservedDate", "ReservedTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FieldId1",
                table: "Reservations",
                column: "FieldId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId1",
                table: "ProductImage",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRatings_RatedPlayerId",
                table: "PlayerRatings",
                column: "RatedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRatings_RaterPlayerId",
                table: "PlayerRatings",
                column: "RaterPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId",
                table: "ProductRatings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_UserId",
                table: "ProductRatings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId1",
                table: "ProductImage",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Fields_FieldId1",
                table: "Reservations",
                column: "FieldId1",
                principalTable: "Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_AspNetUsers_CaptainUserId",
                table: "TournamentRegistrations",
                column: "CaptainUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Payments_PaymentId",
                table: "TournamentRegistrations",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Teams_TeamId",
                table: "TournamentRegistrations",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId",
                table: "TournamentRegistrations",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId1",
                table: "TournamentRegistrations",
                column: "TournamentId1",
                principalTable: "Tournaments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId1",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Fields_FieldId1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_AspNetUsers_CaptainUserId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Payments_PaymentId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Teams_TeamId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId",
                table: "TournamentRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId1",
                table: "TournamentRegistrations");

            migrationBuilder.DropTable(
                name: "PlayerRatings");

            migrationBuilder.DropTable(
                name: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrations_CaptainUserId",
                table: "TournamentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrations_TournamentId_TeamId",
                table: "TournamentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrations_TournamentId1",
                table: "TournamentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TeamInvites_TeamId_PlayerId",
                table: "TeamInvites");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FieldId_ReservedDate_ReservedTime",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FieldId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ProductId1",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "TournamentId1",
                table: "TournamentRegistrations");

            migrationBuilder.DropColumn(
                name: "FieldId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductImage");

            migrationBuilder.AlterColumn<string>(
                name: "CaptainUserId",
                table: "TournamentRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CaptainId",
                table: "TournamentRegistrations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_CaptainId",
                table: "TournamentRegistrations",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_TournamentId",
                table: "TournamentRegistrations",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvites_TeamId",
                table: "TeamInvites",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FieldId",
                table: "Reservations",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_AspNetUsers_CaptainId",
                table: "TournamentRegistrations",
                column: "CaptainId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Payments_PaymentId",
                table: "TournamentRegistrations",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Teams_TeamId",
                table: "TournamentRegistrations",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrations_Tournaments_TournamentId",
                table: "TournamentRegistrations",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

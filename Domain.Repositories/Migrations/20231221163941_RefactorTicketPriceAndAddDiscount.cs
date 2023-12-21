using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTicketPriceAndAddDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreePlaces",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "TicketDiscountPercentage",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Event");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "TicketType",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "DiscountId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPurchased",
                table: "Ticket",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TicketDiscount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDiscount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DiscountId",
                table: "Ticket",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TicketDiscount_DiscountId",
                table: "Ticket",
                column: "DiscountId",
                principalTable: "TicketDiscount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketDiscount_DiscountId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketDiscount");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_DiscountId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TicketType");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "isPurchased",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "FreePlaces",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketDiscountPercentage",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TicketPrice",
                table: "Event",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

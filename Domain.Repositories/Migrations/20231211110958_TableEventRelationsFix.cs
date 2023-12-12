using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class TableEventRelationsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventTable_TableId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_TableId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "TicketPrice",
                table: "EventTable",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "EventTable",
                newName: "TicketPrice");

            migrationBuilder.AddColumn<Guid>(
                name: "TableId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_TableId",
                table: "Event",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventTable_TableId",
                table: "Event",
                column: "TableId",
                principalTable: "EventTable",
                principalColumn: "Id");
        }
    }
}
